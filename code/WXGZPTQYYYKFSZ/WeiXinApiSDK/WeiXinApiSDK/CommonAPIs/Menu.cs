using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;
using Senparc.Weixin.MP.Exceptions;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.CommonAPIs
{
    /// <summary>
    /// 微信公众平台自定义菜单操作类
    /// </summary>
    public class Meun
    {
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="buttonData">菜单内容</param>
        /// <returns></returns>
        public static WxJsonResult CreateMenu(string accessToken, ButtonGroup buttonData)
        {
            //微信公众平台创建自定义菜单接口地址
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";
            return ApiHelper.Post(accessToken, urlFormat, buttonData);
        }
        #region GetMenu
        /// <summary>
        /// 获取当前菜单，如果菜单不存在，将返回null
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GetMenuResult GetMenu(string accessToken)
        {            
            //微信公众平台获取自定义菜单接口地址
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", accessToken);
            var jsonString = HttpUtility.RequestUtility.HttpGet(url, Encoding.UTF8);
            GetMenuResult finalResult;
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                //获取自定义菜单Json结构
                var jsonResult = js.Deserialize<GetMenuResultJson>(jsonString);
                if (jsonResult.menu == null || jsonResult.menu.button.Count == 0)
                {
                    throw new WeixinException(jsonResult.errmsg);
                }
                //将Json结构自定义菜单转换为自定义菜单实体
                finalResult = GetMenuFromJsonResult(jsonResult);
            }
            catch (WeixinException)
            {
                finalResult = null;
            }
            return finalResult;
        }
        /// <summary>
        /// 根据微信返回的Json数据得到可用的GetMenuResult结果
        /// 将Json结构自定义菜单转换为自定义菜单实体
        /// </summary>
        /// <param name="resultFull"></param>
        /// <returns></returns>
        private static GetMenuResult GetMenuFromJsonResult(GetMenuResultJson resultFull)
        {
            GetMenuResult result = null;
            try
            {
                ButtonGroup bg = new ButtonGroup();
                //循环遍历Json结构
                foreach (var rootButton in resultFull.menu.button)
                {
                    if (rootButton.name == null)
                    {
                        continue;//没有设置一级菜单
                    }
                    //可用二级菜单按钮数量
                    var availableSubButton = rootButton.sub_button.Count(z => !string.IsNullOrEmpty(z.name));
                    //一级菜单格式转换
                    if (availableSubButton == 0)
                    {
                        //按钮格式校验
                        if (rootButton.type == null ||
                            (rootButton.type.Equals("CLICK", StringComparison.OrdinalIgnoreCase)
                            && string.IsNullOrEmpty(rootButton.key)))
                        {
                            throw new WeixinMenuException("单击按钮的key不能为空！");
                        }
                        if (rootButton.type.Equals("CLICK", StringComparison.OrdinalIgnoreCase))
                        {
                            //底部单击按钮
                            bg.button.Add(new SingleClickButton()
                            {
                                name = rootButton.name,
                                key = rootButton.key,
                                type = rootButton.type
                            });
                        }
                        else
                        {
                            //底部URL按钮
                            bg.button.Add(new SingleViewButton()
                            {
                                name = rootButton.name,
                                url = rootButton.url,
                                type = rootButton.type
                            });
                        }
                    }
                    else if (availableSubButton < 1)
                    {
                        throw new WeixinMenuException("子菜单至少需要填写1个！");
                    }
                    //二级菜单格式转换
                    else
                    {
                        //底部二级菜单
                        var subButton = new SubButton(rootButton.name);
                        bg.button.Add(subButton);
                        foreach (var subSubButton in rootButton.sub_button)
                        {
                            if (subSubButton.name == null)
                            {
                                continue; //没有设置菜单
                            }                        
                            //按钮格式校验
                            if (subSubButton.type.Equals("CLICK", StringComparison.OrdinalIgnoreCase)
                                && string.IsNullOrEmpty(subSubButton.key))
                            {
                                throw new WeixinMenuException("单击按钮的key不能为空！");
                            }
                            if (subSubButton.type.Equals("CLICK", StringComparison.OrdinalIgnoreCase))
                            {
                                //底部单击按钮
                                subButton.sub_button.Add(new SingleClickButton()
                                {
                                    name = subSubButton.name,
                                    key = subSubButton.key,
                                    type = subSubButton.type
                                });
                            }
                            else
                            {
                                //底部URL按钮
                                subButton.sub_button.Add(new SingleViewButton()
                                {
                                    name = subSubButton.name,
                                    url = subSubButton.url,
                                    type = subSubButton.type
                                });
                            }
                        }
                    }
                }
                if (bg.button.Count < 1)
                {
                    throw new WeixinMenuException("一级菜单按钮至少为1个！");
                }
                //保存到自定义菜单实体
                result = new GetMenuResult()
                {
                    menu = bg
                };
            }
            catch (Exception ex)
            {
                throw new WeixinMenuException(ex.Message, ex);
            }
            return result;
        }
        #endregion
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static WxJsonResult DeleteMenu(string accessToken)
        {
            //微信公众平台删除自定义菜单接口地址
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}";
            var result = ApiHelper.Get(accessToken, urlFormat);
            return result;
        }
    }
}
