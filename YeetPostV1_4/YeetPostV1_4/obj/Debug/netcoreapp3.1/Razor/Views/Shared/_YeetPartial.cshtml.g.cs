#pragma checksum "C:\Users\rjvar\Documents\dot_net\YeetPost\YeetPostV1_4\YeetPostV1_4\Views\Shared\_YeetPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a79c0a97d044ea9abea3f7d55ca346a849b7c9e6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__YeetPartial), @"mvc.1.0.view", @"/Views/Shared/_YeetPartial.cshtml")]
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
#line 1 "C:\Users\rjvar\Documents\dot_net\YeetPost\YeetPostV1_4\YeetPostV1_4\Views\_ViewImports.cshtml"
using YeetPostV1_4;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\rjvar\Documents\dot_net\YeetPost\YeetPostV1_4\YeetPostV1_4\Views\_ViewImports.cshtml"
using YeetPostV1_4.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a79c0a97d044ea9abea3f7d55ca346a849b7c9e6", @"/Views/Shared/_YeetPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"484a98dcb095733bcead2f3b61461a2a5da46caf", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__YeetPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<YeetPostV1_4.ViewModel.YeetViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral(@"
<v-app id=""inspire"">
    <div v-for=""(yeet, index) in model.yeets"">
        <v-card class=""mx-auto""
                color=""#26c6da""
                dark
                max-width=""600"">
            <div>
                <v-card-title>


                    <v-list-item-content>
                        <v-list-item-title> &nbsp; {{yeet.username}}</v-list-item-title>
                    </v-list-item-content>

                    <v-list-item-content>
                        <v-list-item-title> &nbsp; <strong> <b>{{yeet.header}}</b> </strong></v-list-item-title>
                    </v-list-item-content>
                    <v-list-item-content>
                        <v-list-item-title> &nbsp; {{yeet.date}}</v-list-item-title>
                    </v-list-item-content>

                    <v-list-item-content>
                        <v-list-item-title> &nbsp; {{yeet.location}}</v-list-item-title>
                    </v-list-item-content>


                    <v-dialog v-if=""yee");
            WriteLiteral(@"t.isMine == true"" v-model=""yeet.deleteModal"" persistent max-width=""290"">
                        <template v-slot:activator=""{ on }"">
                            <v-list-item-content>
                                <v-icon class=""mr-1"" color=""red"" v-on=""on"">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; mdi-delete</v-icon>
                            </v-list-item-content>
                        </template>
                        <v-card>
                            <div>
                                <v-card-title class=""headline"" style=""text-align: center;"">Are you sure you want to deYeet this Yeet?</v-card-title>
                                <v-card-actions>
                                    <v-spacer></v-spacer>
                                    <v-btn color=""green darken-1"" text v-on:click=""yeet.deleteModal = false"">Close</v-btn>
                                    <v-btn color=""green darken-1"" text
                                           v-on:click=""deleteYeet(yeet.yeetID, yeet.location)");
            WriteLiteral(@""">Yes</v-btn>
                                </v-card-actions>
                            </div>
                            <div>

                            </div>
                        </v-card>
                    </v-dialog>

                    <v-dialog v-if=""yeet.iFlagged == true && yeet.isMine == false"" v-model=""yeet.modal"" persistent max-width=""290"">
                        <template v-slot:activator=""{ on }"">
                            <v-list-item-content>
                                <v-icon class=""mr-1"" color=""red"" v-on=""on"">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; mdi-flag</v-icon>

                            </v-list-item-content>
                        </template>
                        <v-card>
                            <div>
                                <v-card-title class=""headline"" style=""text-align: center;"">Unflag Yeet? {{yeet.yeetID}}</v-card-title>

                                <v-card-actions>
                                    <v-spacer></v-spacer");
            WriteLiteral(@">
                                    <v-btn color=""green darken-1"" text v-on:click=""yeet.modal = false"">Close</v-btn>
                                    <v-btn color=""green darken-1"" text
                                           v-on:click=""flagYeet(yeet.yeetID, yeet.whoFlags,true, inappropriate, model.location )"">Yes</v-btn>
                                </v-card-actions>
                            </div>
                            <div>

                            </div>
                        </v-card>
                    </v-dialog>

                    <v-dialog v-model=""yeet.modal"" persistent max-width=""290"" v-if=""yeet.iFlagged == false && yeet.isMine == false"">
                        <template v-slot:activator=""{ on }"">
                            <v-list-item-content>

                                <v-icon class=""mr-1"" color=""white"" v-on=""on"">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; mdi-flag</v-icon>
                            </v-list-item-content>
                        </");
            WriteLiteral(@"template>
                        <v-card>
                            <v-card-title class=""headline"" style=""text-align: center;"">Flag for What?</v-card-title>

                            <v-row>
                                <v-col cols=""12"">
                                    <v-divider></v-divider>
                                    <v-card-text style=""font-size: 32px; text-align: center;""
                                                 v-on:click=""flagYeet(yeet.yeetID, yeet.whoFlags,false, inappropriate ,model.location )"">Innapropriate Context</v-card-text>
                                    <v-divider></v-divider>
                                </v-col>
                                <v-col cols=""12"">
                                    <v-card-text style=""font-size: 32px;text-align: center;""
                                                 v-on:click=""flagYeet(yeet.yeetID, yeet.whoFlags,false, abusive ,model.location )"">Abusive</v-card-text>
                                    <v-");
            WriteLiteral(@"divider></v-divider>
                                </v-col>

                            </v-row>
                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn color=""green darken-1"" text v-on:click=""yeet.modal = false"">Close</v-btn>
                            </v-card-actions>
                        </v-card>
                    </v-dialog>


                </v-card-title>

                <v-card-text class=""headline font-weight-bold"" v-on:click=""viewYeet(yeet.yeetID)"">
                    ""   {{yeet.yeet}} ""
                </v-card-text>
            </div>
            <v-card-actions>
                <v-list-item class=""grow"">

                    <v-row align=""center""
                           justify=""end"">
                        <v-icon class=""mr-1"" color=""red"" v-if=""yeet.iLiked == true"" v-on:click=""likeYeet(yeet.yeetID, yeet.whoLikes, model.location, true)"">mdi-heart</v-icon>

                  ");
            WriteLiteral(@"      <v-icon class=""mr-1"" v-else v-on:click=""likeYeet(yeet.yeetID, yeet.whoLikes, model.location, false)"">mdi-heart</v-icon>


                        <span class=""subheading mr-2"">{{yeet.totalLikes}}</span>

                    </v-row>
                </v-list-item>
            </v-card-actions>

        </v-card>
        <br />
    </div>
</v-app>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<YeetPostV1_4.ViewModel.YeetViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
