﻿Type.registerNamespace("ECP");function BannedSenderSlab(n){this.$$d_collectionEditorRefreshMethod_Invoking=Function.createDelegate(this,this.collectionEditorRefreshMethod_Invoking);this.$$d_$I_4=Function.createDelegate(this,this.$I_4);this.$$d_$S_4=Function.createDelegate(this,this.$S_4);this.$$d_$T_4=Function.createDelegate(this,this.$T_4);this.$$d_$O_4=Function.createDelegate(this,this.$O_4);this.$$d_$P_4=Function.createDelegate(this,this.$P_4);this.$5_4=[];this.$3_4=[];BannedSenderSlab.initializeBase(this,[n]);this.$6_4=[]}BannedSenderSlab.prototype={$C_4:null,$4_4:null,$1_4:null,$8_4:null,$9_4:null,$G_4:null,$E_4:null,$0_4:null,$2_4:null,$7_4:null,$6_4:null,get_SenderDomainTextArea:function(){return this.$C_4},set_SenderDomainTextArea:function(n){this.$C_4=n;return n},get_BannedSenderCollectionEditor:function(){return this.$8_4},set_BannedSenderCollectionEditor:function(n){this.$8_4=n;return n},get_CollectionEditorRefreshMethod:function(){return this.$9_4},set_CollectionEditorRefreshMethod:function(n){this.$9_4=n;return n},get_ServiceUrl:function(){return this.$7_4},set_ServiceUrl:function(n){this.$7_4=n;return n},get_SearchButton:function(){return this.$4_4},set_SearchButton:function(n){this.$4_4=n;return n},get_ClearButton:function(){return this.$1_4},set_ClearButton:function(n){this.$1_4=n;return n},initialize:function(){EcpControl.prototype.initialize.call(this);this.$G_4=this.$$d_$P_4;this.$E_4=this.$$d_$O_4;$addHandler(this.$4_4,"click",this.$$d_$P_4);$addHandler(this.$1_4,"click",this.$$d_$O_4);this.$0_4=new WebServiceCommand;this.$0_4.set_serviceUrl(this.$7_4);this.$0_4.set_methodName("ListBannedSender");this.$0_4.set_parameterNames(9);this.$0_4.get_webServiceMethod().add_Succeeded(this.$$d_$T_4);this.$0_4.get_webServiceMethod().add_Completed(this.$$d_$S_4);this.$2_4=new WebServiceCommand;this.$2_4.set_serviceUrl(this.$7_4);this.$2_4.set_methodName("DelistBannedSender");this.$2_4.set_parameterNames(4);this.$2_4.get_webServiceMethod().add_Completed(this.$$d_$I_4);this.$9_4.add_Invoking(this.$$d_collectionEditorRefreshMethod_Invoking)},collectionEditorRefreshMethod_Invoking:function(n,t){t.set_cancel(_String.isNullOrEmpty(this.$C_4.value))},dispose:function(){$removeHandler(this.$4_4,"click",this.$G_4);$removeHandler(this.$1_4,"click",this.$E_4);EcpControl.prototype.dispose.call(this)},$S_4:function(n,t){this.$3_4.length>0?this.$M_4(this.$3_4.pop()):this.$L_4()},$I_4:function(n,t){this.$5_4.length>0?this.$F_4(this.$5_4.pop()):this.$K_4()},$T_4:function(n,t){for(var u=t.get_results(),f=u.Output[0],e=f.SenderAddressList,r=e,o=r.length,i=0;i<o;++i){var s=r[i];Array.add(this.$6_4,{SenderAddressList:s})}},$P_4:function(n){_String.isNullOrEmpty(this.$C_4.value)?alert(Strings.get_NoSenderAddressWarning()):this.$H_4()},$M_4:function(n){var t=this;this.$0_4.set_ParameterGetter(function(){return[null,n]});this.$0_4.execute()},$F_4:function(n){var t=this;this.$2_4.set_ParameterGetter(function(){return[null,n]});this.$2_4.execute()},$O_4:function(n){if(this.$8_4.get_ListView().get_items().length>0){this.$1_4.disabled=!0;this.$R_4()}},$R_4:function(){for(var e=[],t=this.$8_4.get_ListView().get_selectedItems(),r=t.length,n=0;n<r;++n){var u=t[n];var f=u.get_dataObject();var i={};i.SenderAddress=f.SenderAddressList;this.$5_4.push(i)}this.$6_4=[];UpdateProgressPopUp.showProgressImmediately(Strings.get_JobSubmissionWaitText());this.$F_4(this.$5_4.pop())},$K_4:function(){UpdateProgressPopUp.progressCompleted(null,!0);this.$H_4()},$H_4:function(){this.$1_4.disabled=!0;this.$4_4.disabled=!0;this.$6_4=[];for(var t=this.$C_4.value.toString().split(","),r=t.length,n=0;n<r;++n){var u=t[n];var i={};i.SenderAddress=u.trim();this.$3_4.push(i)}if(this.$3_4.length>0){UpdateProgressPopUp.showProgressImmediately(Strings.get_JobSubmissionWaitText());this.$M_4(this.$3_4.pop())}},$L_4:function(){this.$8_4.set_value(this.$6_4);UpdateProgressPopUp.progressCompleted(null,!0);this.$1_4.disabled=!this.$8_4.get_value().length;this.$4_4.disabled=!1}};function EditSpamContentFilterViewModel(){this.$J_4=new RequiredFieldValidator(new ValidatorInfo);EditSpamContentFilterViewModel.initializeBase(this)}EditSpamContentFilterViewModel.prototype={get_LanguageListVM:function(){return this.getValue("languageList")},set_LanguageListVM:function(n){this.setValue("languageList",n);this.makeDirty("languageList");this.$J_4.validate(n);return n},get_RegionListVM:function(){return this.getValue("regionList")},set_RegionListVM:function(n){this.setValue("regionList",n);this.makeDirty("regionList");this.$J_4.validate(n);return n},get_SpamActionOption:function(){return this.getValue("SpamAction")},set_SpamActionOption:function(n){this.setValue("SpamAction",n);this.raisePropertyChanged("XHeaderEnabled");this.raisePropertyChanged("PrependSubjectEnabled");this.raisePropertyChanged("RedirectMessageEnabled");return n},get_HighConfidenceSpamActionOption:function(){return this.getValue("HighConfidenceSpamAction")},set_HighConfidenceSpamActionOption:function(n){this.setValue("HighConfidenceSpamAction",n);this.raisePropertyChanged("XHeaderEnabled");this.raisePropertyChanged("PrependSubjectEnabled");this.raisePropertyChanged("RedirectMessageEnabled");return n},get_XHeaderEnabled:function(){return this.get_SpamActionOption()==="AddXHeader"||this.get_HighConfidenceSpamActionOption()==="AddXHeader"},get_PrependSubjectEnabled:function(){return this.get_SpamActionOption()==="ModifySubject"||this.get_HighConfidenceSpamActionOption()==="ModifySubject"},get_RedirectMessageEnabled:function(){return this.get_SpamActionOption()==="Redirect"||this.get_HighConfidenceSpamActionOption()==="Redirect"},get_conditions:function(){return this.get_values()},set_conditions:function(n){this.$Q_4(n);return n},get_exceptions:function(){return this.get_values()},set_exceptions:function(n){this.$Q_4(n);return n},validateSave:function(){this.makeDirty("PolicyIdentity");return PropertyPageViewModel.prototype.validateSave.call(this)},$Q_4:function(n){var e=n;for(var o in e){var i={key:o,value:e[o]};var s=this.getValue(i.key);var t=i.value;if(s&&Array.isInstanceOfType(s)&&null===t)t=new Array(0);else if(Array.isInstanceOfType(t)){for(var r=[],h=t,l=h.length,u=0;u<l;++u){var f=h[u];if(PeopleIdentity.isInstanceOfType(f)){var c=f;Array.add(r,new PeopleIdentityWithType(c.SMTPAddress,c.DisplayName))}else Array.add(r,f.toString())}t=r}this.setValue(i.key,t)}}};function IPDelistingSlab(n){this.$$d_collectionEditorRefreshMethod_Invoking=Function.createDelegate(this,this.collectionEditorRefreshMethod_Invoking);this.$$d_$I_4=Function.createDelegate(this,this.$I_4);this.$$d_$S_4=Function.createDelegate(this,this.$S_4);this.$$d_$T_4=Function.createDelegate(this,this.$T_4);this.$$d_$O_4=Function.createDelegate(this,this.$O_4);this.$$d_$P_4=Function.createDelegate(this,this.$P_4);this.$5_4=[];this.$3_4=[];IPDelistingSlab.initializeBase(this,[n]);this.$6_4=[]}IPDelistingSlab.prototype={$B_4:null,$4_4:null,$1_4:null,$A_4:null,$9_4:null,$G_4:null,$E_4:null,$0_4:null,$2_4:null,$7_4:null,$6_4:null,get_IPListTextArea:function(){return this.$B_4},set_IPListTextArea:function(n){this.$B_4=n;return n},get_IPDelistingCollectionEditor:function(){return this.$A_4},set_IPDelistingCollectionEditor:function(n){this.$A_4=n;return n},get_CollectionEditorRefreshMethod:function(){return this.$9_4},set_CollectionEditorRefreshMethod:function(n){this.$9_4=n;return n},get_ServiceUrl:function(){return this.$7_4},set_ServiceUrl:function(n){this.$7_4=n;return n},get_SearchButton:function(){return this.$4_4},set_SearchButton:function(n){this.$4_4=n;return n},get_ClearButton:function(){return this.$1_4},set_ClearButton:function(n){this.$1_4=n;return n},initialize:function(){EcpControl.prototype.initialize.call(this);this.$G_4=this.$$d_$P_4;this.$E_4=this.$$d_$O_4;$addHandler(this.$4_4,"click",this.$$d_$P_4);$addHandler(this.$1_4,"click",this.$$d_$O_4);this.$0_4=new WebServiceCommand;this.$0_4.set_serviceUrl(this.$7_4);this.$0_4.set_methodName("ListIP");this.$0_4.set_parameterNames(9);this.$0_4.get_webServiceMethod().add_Succeeded(this.$$d_$T_4);this.$0_4.get_webServiceMethod().add_Completed(this.$$d_$S_4);this.$2_4=new WebServiceCommand;this.$2_4.set_serviceUrl(this.$7_4);this.$2_4.set_methodName("DelistIP");this.$2_4.set_parameterNames(4);this.$2_4.get_webServiceMethod().add_Completed(this.$$d_$I_4);this.$9_4.add_Invoking(this.$$d_collectionEditorRefreshMethod_Invoking)},collectionEditorRefreshMethod_Invoking:function(n,t){t.set_cancel(_String.isNullOrEmpty(this.$B_4.value))},dispose:function(){$removeHandler(this.$4_4,"click",this.$G_4);$removeHandler(this.$1_4,"click",this.$E_4);EcpControl.prototype.dispose.call(this)},$S_4:function(n,t){this.$3_4.length>0?this.$N_4(this.$3_4.pop()):this.$L_4()},$I_4:function(n,t){this.$5_4.length>0?this.$F_4(this.$5_4.pop()):this.$K_4()},$T_4:function(n,t){var u=t.get_results();var i=u.Output[0];if(i.DnsBlockListResponse[0]){var r=i.ListNames;if(_Object.isNullOrUndefined(r)||!r.length)i.ListNames=[i.ExternalList.toString()];else{Array.add(r,i.ExternalList.toString());i.ListNames=r}}Array.add(this.$6_4,u.Output[0])},$P_4:function(n){_String.isNullOrEmpty(this.$B_4.value)?alert(Strings.get_NoIPWarning()):this.$H_4()},$N_4:function(n){var t=this;this.$0_4.set_ParameterGetter(function(){return[null,n]});this.$0_4.execute()},$F_4:function(n){if(n.DaysSinceLastDelist>90){var t=this;this.$2_4.set_ParameterGetter(function(){return[null,n]});this.$2_4.execute()}else this.$I_4(this,null)},$O_4:function(n){if(this.$A_4.get_ListView().get_items().length>0){this.$1_4.disabled=!0;for(var o=[],u=this.$A_4.get_ListView().get_selectedItems(),f=u.length,i=0;i<f;++i){var e=u[i];var r=e.get_dataObject();var t={};t.IPToDelist=r.IpAddress;t.ListNames=r.ListNames.toString().split(",");t.DaysSinceLastDelist=r.DaysSinceLastDelist;this.$5_4.push(t)}this.$R_4()}},$R_4:function(){this.$6_4=[];UpdateProgressPopUp.showProgressImmediately(Strings.get_JobSubmissionWaitText());this.$F_4(this.$5_4.pop())},$K_4:function(){UpdateProgressPopUp.progressCompleted(null,!0);this.$H_4()},$H_4:function(){this.$6_4=[];for(var u=new RegExp("^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}(/([0-9]|[1-2][0-9]|3[0-2]))?$"),t=this.$B_4.value.toString().split(","),f=t.length,n=0;n<f;++n){var i=t[n];if(u.test(i.trim())){var r={};r.IPAddress=i.trim();this.$3_4.push(r)}}if(this.$3_4.length>0){this.$1_4.disabled=!0;this.$4_4.disabled=!0;UpdateProgressPopUp.showProgressImmediately(Strings.get_JobSubmissionWaitText());this.$N_4(this.$3_4.pop())}},$L_4:function(){var n=this.$6_4;this.$A_4.set_value(n);UpdateProgressPopUp.progressCompleted(null,!0);this.$1_4.disabled=!this.$A_4.get_value().length;this.$4_4.disabled=!1}};BannedSenderSlab.registerClass("BannedSenderSlab",ContainerControl);EditSpamContentFilterViewModel.registerClass("EditSpamContentFilterViewModel",PropertyPageViewModel);IPDelistingSlab.registerClass("IPDelistingSlab",ContainerControl)