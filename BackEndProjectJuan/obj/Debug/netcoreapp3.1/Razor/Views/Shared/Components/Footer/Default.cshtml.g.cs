#pragma checksum "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\Components\Footer\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7abb329c3d171202b45813b885114c2373357463"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Footer_Default), @"mvc.1.0.view", @"/Views/Shared/Components/Footer/Default.cshtml")]
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
using BackEndProjectJuan.ViewModels.Home;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\_ViewImports.cshtml"
using BackEndProjectJuan.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\_ViewImports.cshtml"
using BackEndProjectJuan.Interfaces;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7abb329c3d171202b45813b885114c2373357463", @"/Views/Shared/Components/Footer/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"382ac41950e9f9c47599b6f31444d44ea709f438", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_Footer_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IDictionary<string,string>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"    
    <!-- Start Footer Area Wrapper -->
<footer class=""footer-wrapper"">
    <!-- footer main area start -->
    <div class=""footer-widget-area section-padding"">
        <div class=""container"">
            <div class=""row mtn-40"">
                <!-- footer widget item start -->
                <div class=""col-xl-5 col-lg-3 col-md-6"">
                    <div class=""widget-item mt-40"">
                        <h5 class=""widget-title"">My Account</h5>
                        <div class=""widget-body"">
                            <ul class=""location-wrap"">
                                <li><i class=""ion-ios-location-outline""></i>");
#nullable restore
#line 15 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\Components\Footer\Default.cshtml"
                                                                       Write(Model.FirstOrDefault(m=>m.Key=="Address").Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n                                <li><i class=\"ion-ios-email-outline\"></i>Mail Us: <a");
            BeginWriteAttribute("href", " href=\"", 827, "\"", 887, 2);
            WriteAttributeValue("", 834, "mailto:", 834, 7, true);
#nullable restore
#line 16 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\Components\Footer\Default.cshtml"
WriteAttributeValue("", 841, Model.FirstOrDefault(m=>m.Key=="Email").Value, 841, 46, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 16 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\Components\Footer\Default.cshtml"
                                                                                                                                             Write(Model.FirstOrDefault(m=>m.Key=="Email").Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n                                <li><i class=\"ion-ios-telephone-outline\"></i>Phone: <a href=\"%2b0025425456554\">");
#nullable restore
#line 17 "C:\Users\user\Documents\BackEndProjectJuan\BackEndProjectJuan\Views\Shared\Components\Footer\Default.cshtml"
                                                                                                          Write(Model.FirstOrDefault(m => m.Key == "Phone").Value);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- footer widget item end -->
                <!-- footer widget item start -->
                <div class=""col-xl-2 col-lg-3 col-md-6"">
                    <div class=""widget-item mt-40"">
                        <h5 class=""widget-title"">Categories</h5>
                        <div class=""widget-body"">
                            <ul class=""useful-link"">
                                <li><a href=""#"">Ecommerce</a></li>
                                <li><a href=""#"">Shopify</a></li>
                                <li><a href=""#"">Prestashop</a></li>
                                <li><a href=""#"">Opencart</a></li>
                                <li><a href=""#"">Magento</a></li>
                                <li><a href=""#"">Jigoshop</a></li>
                            </ul>
                        </div>
                    </div>
              ");
            WriteLiteral(@"  </div>
                <!-- footer widget item end -->
                <!-- footer widget item start -->
                <div class=""col-xl-2 col-lg-3 col-md-6"">
                    <div class=""widget-item mt-40"">
                        <h5 class=""widget-title"">Information</h5>
                        <div class=""widget-body"">
                            <ul class=""useful-link"">
                                <li><a href=""#"">Home</a></li>
                                <li><a href=""#"">About Us</a></li>
                                <li><a href=""#"">Contact Us</a></li>
                                <li><a href=""#"">Returns & Exchanges</a></li>
                                <li><a href=""#"">Shipping & Delivery</a></li>
                                <li><a href=""#"">Privacy Policy</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- footer widget item end -->
                <!-- footer widget ");
            WriteLiteral(@"item start -->
                <div class=""col-xl-2 col-lg-3 offset-xl-1 col-md-6"">
                    <div class=""widget-item mt-40"">
                        <h5 class=""widget-title"">Quick Links</h5>
                        <div class=""widget-body"">
                            <ul class=""useful-link"">
                                <li><a href=""#"">Store Location</a></li>
                                <li><a href=""#"">My Account</a></li>
                                <li><a href=""#"">Orders Tracking</a></li>
                                <li><a href=""#"">Size Guide</a></li>
                                <li><a href=""#"">Shopping Rates</a></li>
                                <li><a href=""#"">Contact Us</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- footer widget item end -->
            </div>
        </div>
    </div>
    <!-- footer main area end -->
    <!-- footer bottom area start -");
            WriteLiteral(@"->
    <div class=""footer-bottom"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-md-6 order-2 order-md-1"">
                    <div class=""copyright-text text-center text-md-left"">
                        <p>Copyright 2019 <a href=""index.html"">Juan</a>. All Rights Reserved</p>
                    </div>
                </div>
                <div class=""col-md-6 order-1 order-md-2"">
                    <div class=""footer-social-link text-center text-md-right"">
                        <a href=""#""><i class=""fa fa-facebook""></i></a>
                        <a href=""#""><i class=""fa fa-twitter""></i></a>
                        <a href=""#""><i class=""fa fa-linkedin""></i></a>
                        <a href=""#""><i class=""fa fa-instagram""></i></a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- footer bottom area end -->
</footer>
<!-- End Footer Area Wrapper -->");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IDictionary<string,string>> Html { get; private set; }
    }
}
#pragma warning restore 1591
