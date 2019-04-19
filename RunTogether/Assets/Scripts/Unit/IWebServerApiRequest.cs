using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using BestHTTP;
using UIScripts;
using UnityEngine;

namespace Unit
{
    public class WebServerApiRequest
    {
        protected readonly Action<string> SuccessedCallback;
        protected readonly Action<string> FailedCallback;
        protected readonly Dictionary<string, string> RequestParamaters;
        public RequestResultEnum RequestResult;

        public WebServerApiRequest()
        {
        }

        public WebServerApiRequest(Dictionary<string, string> parmaters, Action<string> successedCallback = null,
            Action<string> failedCallback = null)
        {
            RequestParamaters = parmaters;
            SuccessedCallback = successedCallback;
            FailedCallback = failedCallback;
        }


        public virtual void Request(HTTPMethods httpMethods = HTTPMethods.Post)
        {
            Debug.Assert(RequestParamaters.ContainsKey("url"), "Paramaters is no contain url key");
            HTTPRequest tmpRequest = new HTTPRequest(new Uri(RequestParamaters["url"]), httpMethods, OnFinished);

            if (httpMethods == HTTPMethods.Post)
            {
                string tmpSign = string.Empty;
                //建立post 表单
                foreach (KeyValuePair<string, string> filed in RequestParamaters)
                {
                    if (filed.Key == "url") continue;
                    tmpRequest.AddField(filed.Key, filed.Value);
                    //将字符串转为url code
                    tmpSign += filed.Key + "=" + UrlEncode(filed.Value) + "&";
                }

                //设置签名
                tmpSign = tmpSign.Remove(tmpSign.LastIndexOf('&'));
                Debug.Log(tmpSign);
                tmpSign = EncryptString(tmpSign + "Runtogether2018");
                tmpRequest.AddField("sign", tmpSign);
            }

            tmpRequest.Send();
        }

        /// <summary>
        /// 将中文重新编码为URLCode，但由于C#URLCode是小写
        /// PHPURLCode是大写故这边需要将其大写话
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>URL Code</returns>
        private string UrlEncode(string value)
        {
            string urlStr = HttpUtility.UrlEncode(value);
            var urlCode = Regex.Matches(urlStr, "%[a-f0-9]{2}", RegexOptions.Compiled).Cast<Match>()
                .Select(m => m.Value).Distinct();
            foreach (string item in urlCode)
            {
                urlStr = urlStr.Replace(item, item.ToUpper());
            }

            return urlStr;
        }

        private void OnFinished(HTTPRequest originalrequest, HTTPResponse response)
        {
            if (response == null) return;
            if (response.IsSuccess)
            {
                SuccessedCallback?.Invoke(response.DataAsText);
            }
            else
            {
                FailedCallback?.Invoke(response.DataAsText);
            }
        }


        private string EncryptString(string str)
        {
            MD5 md5 = MD5.Create();
            // 将字符串转换成字节数组
            byte[] byteOld = Encoding.UTF8.GetBytes(str);
            // 调用加密方法
            byte[] byteNew = md5.ComputeHash(byteOld);
            // 将加密结果转换为字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteNew)
            {
                // 将字节转换成16进制表示的字符串，
                sb.Append(b.ToString("x2"));
            }

            // 返回加密的字符串
            return sb.ToString();
        }
    }
}