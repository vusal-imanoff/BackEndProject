#pragma checksum "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\_BasketPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d06b0436d4e31eb0fab6f9588608bc8897c2e426"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__BasketPartial), @"mvc.1.0.view", @"/Views/Shared/_BasketPartial.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\_ViewImports.cshtml"
using BackEndProjectJuan;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\_ViewImports.cshtml"
using BackEndProjectJuan.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\_ViewImports.cshtml"
using BackEndProjectJuan.ViewModels.AccountViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\_ViewImports.cshtml"
using BackEndProjectJuan.ViewModels.Basket;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\_ViewImports.cshtml"
using BackEndProjectJuan.Interfaces;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d06b0436d4e31eb0fab6f9588608bc8897c2e426", @"/Views/Shared/_BasketPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0527f085d4f9c49a1133377e220ffda940ec3637", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__BasketPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<BasketVM>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("product"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "basket", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteFromBasket", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("minicart-remove deletebasket"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\_BasketPartial.cshtml"
  
    double totalprice = Model.Sum(s => s.Price * s.Count);
    double totalextac = Model.Sum(s => s.Extax * s.Count);
    double totalvat = totalprice*0.2;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    <!-- offcanvas mini cart start -->
<div class=""offcanvas-minicart-wrapper"">
    <div class=""minicart-inner "">
        <div class=""offcanvas-overlay""></div>
        <div class=""minicart-inner-content"">
            <div class=""minicart-close"">
                <i class=""ion-android-close""></i>
            </div>
            <div class=""minicart-content-box header-cart"">
                <div class=""minicart-item-wrapper"">
                    <ul>
");
#nullable restore
#line 20 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\_BasketPartial.cshtml"
                         foreach (BasketVM basket in Model)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <li class=\"minicart-item\">\r\n                                <div class=\"minicart-thumb\">\r\n                                    <a href=\"product-details.html\">\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "d06b0436d4e31eb0fab6f9588608bc8897c2e4266981", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 980, "~/assets/img/product/", 980, 21, true);
#nullable restore
#line 25 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\_BasketPartial.cshtml"
AddHtmlAttributeValue("", 1001, basket.Image, 1001, 13, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                    </a>
                                </div>
                                <div class=""minicart-content"">
                                    <h3 class=""product-name"">
                                        <a href=""product-details.html"">");
#nullable restore
#line 30 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\_BasketPartial.cshtml"
                                                                  Write(basket.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                                    </h3>\r\n                                    <p>\r\n                                        <span class=\"cart-quantity\">");
#nullable restore
#line 33 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\_BasketPartial.cshtml"
                                                               Write(basket.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <strong>&times;</strong></span>\r\n                                        <span class=\"cart-price\">$");
#nullable restore
#line 34 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\_BasketPartial.cshtml"
                                                             Write(basket.Price.ToString("F2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                    </p>\r\n                                </div>\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d06b0436d4e31eb0fab6f9588608bc8897c2e42610155", async() => {
                WriteLiteral("<i class=\"ion-android-close\"></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 37 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\_BasketPartial.cshtml"
                                                                                           WriteLiteral(basket.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            </li>\r\n");
#nullable restore
#line 39 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\_BasketPartial.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    </ul>
                </div>

                <div class=""minicart-pricing-box"">
                    <ul>
                        <li>
                            <span>sub-total</span>
                            <span><strong>$");
#nullable restore
#line 47 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\_BasketPartial.cshtml"
                                      Write(totalprice.ToString("F2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></span>\r\n                        </li>\r\n                        <li>\r\n                            <span>Extax)</span>\r\n                            <span><strong>$");
#nullable restore
#line 51 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\_BasketPartial.cshtml"
                                      Write(totalextac.ToString("F2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></span>\r\n                        </li>\r\n                        <li>\r\n                            <span>VAT (20%)</span>\r\n                            <span><strong>$");
#nullable restore
#line 55 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\_BasketPartial.cshtml"
                                      Write(totalvat.ToString("F2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></span>\r\n                        </li>\r\n                        <li class=\"total\">\r\n                            <span>total</span>\r\n                            <span><strong>$");
#nullable restore
#line 59 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\_BasketPartial.cshtml"
                                       Write((totalvat+totalprice+totalextac).ToString("F2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></span>\r\n                        </li>\r\n                    </ul>\r\n                </div>\r\n\r\n                <div class=\"minicart-button\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d06b0436d4e31eb0fab6f9588608bc8897c2e42615113", async() => {
                WriteLiteral("<i class=\"fa fa-shopping-cart\"></i> view cart");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    <a href=\"cart.html\"><i class=\"fa fa-share\"></i> checkout</a>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<!-- offcanvas mini cart end -->");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<BasketVM>> Html { get; private set; }
    }
}
#pragma warning restore 1591
