using System.ComponentModel;
using Bing.AspNetCore.Apis;
using Bing.AspNetCore.Uploads;
using Bing.Webs.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Bing.Samples.Controllers
{
    /// <summary>
    /// 资源 控制器
    /// </summary>
    [Description("资源控制器")]
    public class ResourceController : ApiControllerBase
    {
        /// <summary>
        /// 初始化一个<see cref="ResourceController"/>类型的实例
        /// </summary>
        /// <param name="apiInterfaceService">Api接口服务</param>
        /// <param name="fileUploadService">文件上传服务</param>
        public ResourceController(
            IApiInterfaceService apiInterfaceService,
            IFileUploadService fileUploadService)
        {
            ApiInterfaceService = apiInterfaceService;
            FileUploadService = fileUploadService;
        }

        /// <summary>
        /// Api接口服务
        /// </summary>
        public IApiInterfaceService ApiInterfaceService { get; set; }

        /// <summary>
        /// 文件上传服务
        /// </summary>
        public IFileUploadService FileUploadService { get; set; }

        /// <summary>
        /// 获取所有控制器
        /// </summary>
        [HttpGet("getAllController")]
        [Description("获取所有控制器")]
        public IActionResult GetAllController() => Success(ApiInterfaceService.GetAllController());

        /// <summary>
        /// 获取所有操作
        /// </summary>
        [HttpGet("getAllAction")]
        [Description("获取所有操作")]
        public IActionResult GetAllAction() => Success(ApiInterfaceService.GetAllAction());

    }
}
