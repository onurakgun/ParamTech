using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ParamTech.Core.Persistence.Extentions.HtmlExtentions;
public static class Helpers
{
    public static HtmlString Nonce(this IHtmlHelper helper)
    {
        return new HtmlString(helper.ViewContext.HttpContext.Items["ScriptNonce"].ToString());
    }
}