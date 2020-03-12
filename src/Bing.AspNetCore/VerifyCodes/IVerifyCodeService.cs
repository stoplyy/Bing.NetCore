namespace Bing.AspNetCore.VerifyCodes
{
    /// <summary>
    /// 验证码服务
    /// </summary>
    public interface IVerifyCodeService
    {
        /// <summary>
        /// 校验验证码有效性
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="id">验证码编号</param>
        /// <param name="removeIfSuccess">验证成功时是否移除</param>
        /// <returns></returns>
        bool CheckCode(string code, string id, bool removeIfSuccess = true);

        /// <summary>
        /// 设置验证码到Session中
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="id">验证码编号</param>
        void SetCode(string code, out string id);
    }
}
