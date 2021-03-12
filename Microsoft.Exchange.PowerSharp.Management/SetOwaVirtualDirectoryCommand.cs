using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200098F RID: 2447
	public class SetOwaVirtualDirectoryCommand : SyntheticCommandWithPipelineInputNoOutput<ADOwaVirtualDirectory>
	{
		// Token: 0x06007A63 RID: 31331 RVA: 0x000B6893 File Offset: 0x000B4A93
		private SetOwaVirtualDirectoryCommand() : base("Set-OwaVirtualDirectory")
		{
		}

		// Token: 0x06007A64 RID: 31332 RVA: 0x000B68A0 File Offset: 0x000B4AA0
		public SetOwaVirtualDirectoryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007A65 RID: 31333 RVA: 0x000B68AF File Offset: 0x000B4AAF
		public virtual SetOwaVirtualDirectoryCommand SetParameters(SetOwaVirtualDirectoryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007A66 RID: 31334 RVA: 0x000B68B9 File Offset: 0x000B4AB9
		public virtual SetOwaVirtualDirectoryCommand SetParameters(SetOwaVirtualDirectoryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000990 RID: 2448
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170052FE RID: 21246
			// (set) Token: 0x06007A67 RID: 31335 RVA: 0x000B68C3 File Offset: 0x000B4AC3
			public virtual string DefaultDomain
			{
				set
				{
					base.PowerSharpParameters["DefaultDomain"] = value;
				}
			}

			// Token: 0x170052FF RID: 21247
			// (set) Token: 0x06007A68 RID: 31336 RVA: 0x000B68D6 File Offset: 0x000B4AD6
			public virtual bool DigestAuthentication
			{
				set
				{
					base.PowerSharpParameters["DigestAuthentication"] = value;
				}
			}

			// Token: 0x17005300 RID: 21248
			// (set) Token: 0x06007A69 RID: 31337 RVA: 0x000B68EE File Offset: 0x000B4AEE
			public virtual bool FormsAuthentication
			{
				set
				{
					base.PowerSharpParameters["FormsAuthentication"] = value;
				}
			}

			// Token: 0x17005301 RID: 21249
			// (set) Token: 0x06007A6A RID: 31338 RVA: 0x000B6906 File Offset: 0x000B4B06
			public virtual bool OAuthAuthentication
			{
				set
				{
					base.PowerSharpParameters["OAuthAuthentication"] = value;
				}
			}

			// Token: 0x17005302 RID: 21250
			// (set) Token: 0x06007A6B RID: 31339 RVA: 0x000B691E File Offset: 0x000B4B1E
			public virtual bool AdfsAuthentication
			{
				set
				{
					base.PowerSharpParameters["AdfsAuthentication"] = value;
				}
			}

			// Token: 0x17005303 RID: 21251
			// (set) Token: 0x06007A6C RID: 31340 RVA: 0x000B6936 File Offset: 0x000B4B36
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x17005304 RID: 21252
			// (set) Token: 0x06007A6D RID: 31341 RVA: 0x000B694E File Offset: 0x000B4B4E
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x17005305 RID: 21253
			// (set) Token: 0x06007A6E RID: 31342 RVA: 0x000B6966 File Offset: 0x000B4B66
			public virtual bool LiveIdAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdAuthentication"] = value;
				}
			}

			// Token: 0x17005306 RID: 21254
			// (set) Token: 0x06007A6F RID: 31343 RVA: 0x000B697E File Offset: 0x000B4B7E
			public virtual GzipLevel GzipLevel
			{
				set
				{
					base.PowerSharpParameters["GzipLevel"] = value;
				}
			}

			// Token: 0x17005307 RID: 21255
			// (set) Token: 0x06007A70 RID: 31344 RVA: 0x000B6996 File Offset: 0x000B4B96
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x17005308 RID: 21256
			// (set) Token: 0x06007A71 RID: 31345 RVA: 0x000B69AE File Offset: 0x000B4BAE
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x17005309 RID: 21257
			// (set) Token: 0x06007A72 RID: 31346 RVA: 0x000B69C1 File Offset: 0x000B4BC1
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x1700530A RID: 21258
			// (set) Token: 0x06007A73 RID: 31347 RVA: 0x000B69D4 File Offset: 0x000B4BD4
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x1700530B RID: 21259
			// (set) Token: 0x06007A74 RID: 31348 RVA: 0x000B69E7 File Offset: 0x000B4BE7
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x1700530C RID: 21260
			// (set) Token: 0x06007A75 RID: 31349 RVA: 0x000B69FA File Offset: 0x000B4BFA
			public virtual MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["ExternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x1700530D RID: 21261
			// (set) Token: 0x06007A76 RID: 31350 RVA: 0x000B6A0D File Offset: 0x000B4C0D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700530E RID: 21262
			// (set) Token: 0x06007A77 RID: 31351 RVA: 0x000B6A20 File Offset: 0x000B4C20
			public virtual bool? DirectFileAccessOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["DirectFileAccessOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x1700530F RID: 21263
			// (set) Token: 0x06007A78 RID: 31352 RVA: 0x000B6A38 File Offset: 0x000B4C38
			public virtual bool? DirectFileAccessOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["DirectFileAccessOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17005310 RID: 21264
			// (set) Token: 0x06007A79 RID: 31353 RVA: 0x000B6A50 File Offset: 0x000B4C50
			public virtual bool? WebReadyDocumentViewingOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x17005311 RID: 21265
			// (set) Token: 0x06007A7A RID: 31354 RVA: 0x000B6A68 File Offset: 0x000B4C68
			public virtual bool? WebReadyDocumentViewingOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17005312 RID: 21266
			// (set) Token: 0x06007A7B RID: 31355 RVA: 0x000B6A80 File Offset: 0x000B4C80
			public virtual bool? ForceWebReadyDocumentViewingFirstOnPublicComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWebReadyDocumentViewingFirstOnPublicComputers"] = value;
				}
			}

			// Token: 0x17005313 RID: 21267
			// (set) Token: 0x06007A7C RID: 31356 RVA: 0x000B6A98 File Offset: 0x000B4C98
			public virtual bool? ForceWebReadyDocumentViewingFirstOnPrivateComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWebReadyDocumentViewingFirstOnPrivateComputers"] = value;
				}
			}

			// Token: 0x17005314 RID: 21268
			// (set) Token: 0x06007A7D RID: 31357 RVA: 0x000B6AB0 File Offset: 0x000B4CB0
			public virtual bool? WacViewingOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WacViewingOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x17005315 RID: 21269
			// (set) Token: 0x06007A7E RID: 31358 RVA: 0x000B6AC8 File Offset: 0x000B4CC8
			public virtual bool? WacViewingOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WacViewingOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17005316 RID: 21270
			// (set) Token: 0x06007A7F RID: 31359 RVA: 0x000B6AE0 File Offset: 0x000B4CE0
			public virtual bool? ForceWacViewingFirstOnPublicComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWacViewingFirstOnPublicComputers"] = value;
				}
			}

			// Token: 0x17005317 RID: 21271
			// (set) Token: 0x06007A80 RID: 31360 RVA: 0x000B6AF8 File Offset: 0x000B4CF8
			public virtual bool? ForceWacViewingFirstOnPrivateComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWacViewingFirstOnPrivateComputers"] = value;
				}
			}

			// Token: 0x17005318 RID: 21272
			// (set) Token: 0x06007A81 RID: 31361 RVA: 0x000B6B10 File Offset: 0x000B4D10
			public virtual RemoteDocumentsActions? RemoteDocumentsActionForUnknownServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsActionForUnknownServers"] = value;
				}
			}

			// Token: 0x17005319 RID: 21273
			// (set) Token: 0x06007A82 RID: 31362 RVA: 0x000B6B28 File Offset: 0x000B4D28
			public virtual AttachmentBlockingActions? ActionForUnknownFileAndMIMETypes
			{
				set
				{
					base.PowerSharpParameters["ActionForUnknownFileAndMIMETypes"] = value;
				}
			}

			// Token: 0x1700531A RID: 21274
			// (set) Token: 0x06007A83 RID: 31363 RVA: 0x000B6B40 File Offset: 0x000B4D40
			public virtual MultiValuedProperty<string> WebReadyFileTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyFileTypes"] = value;
				}
			}

			// Token: 0x1700531B RID: 21275
			// (set) Token: 0x06007A84 RID: 31364 RVA: 0x000B6B53 File Offset: 0x000B4D53
			public virtual MultiValuedProperty<string> WebReadyMimeTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyMimeTypes"] = value;
				}
			}

			// Token: 0x1700531C RID: 21276
			// (set) Token: 0x06007A85 RID: 31365 RVA: 0x000B6B66 File Offset: 0x000B4D66
			public virtual bool? WebReadyDocumentViewingForAllSupportedTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingForAllSupportedTypes"] = value;
				}
			}

			// Token: 0x1700531D RID: 21277
			// (set) Token: 0x06007A86 RID: 31366 RVA: 0x000B6B7E File Offset: 0x000B4D7E
			public virtual MultiValuedProperty<string> WebReadyDocumentViewingSupportedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingSupportedMimeTypes"] = value;
				}
			}

			// Token: 0x1700531E RID: 21278
			// (set) Token: 0x06007A87 RID: 31367 RVA: 0x000B6B91 File Offset: 0x000B4D91
			public virtual MultiValuedProperty<string> WebReadyDocumentViewingSupportedFileTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingSupportedFileTypes"] = value;
				}
			}

			// Token: 0x1700531F RID: 21279
			// (set) Token: 0x06007A88 RID: 31368 RVA: 0x000B6BA4 File Offset: 0x000B4DA4
			public virtual MultiValuedProperty<string> AllowedFileTypes
			{
				set
				{
					base.PowerSharpParameters["AllowedFileTypes"] = value;
				}
			}

			// Token: 0x17005320 RID: 21280
			// (set) Token: 0x06007A89 RID: 31369 RVA: 0x000B6BB7 File Offset: 0x000B4DB7
			public virtual MultiValuedProperty<string> AllowedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["AllowedMimeTypes"] = value;
				}
			}

			// Token: 0x17005321 RID: 21281
			// (set) Token: 0x06007A8A RID: 31370 RVA: 0x000B6BCA File Offset: 0x000B4DCA
			public virtual MultiValuedProperty<string> ForceSaveFileTypes
			{
				set
				{
					base.PowerSharpParameters["ForceSaveFileTypes"] = value;
				}
			}

			// Token: 0x17005322 RID: 21282
			// (set) Token: 0x06007A8B RID: 31371 RVA: 0x000B6BDD File Offset: 0x000B4DDD
			public virtual MultiValuedProperty<string> ForceSaveMimeTypes
			{
				set
				{
					base.PowerSharpParameters["ForceSaveMimeTypes"] = value;
				}
			}

			// Token: 0x17005323 RID: 21283
			// (set) Token: 0x06007A8C RID: 31372 RVA: 0x000B6BF0 File Offset: 0x000B4DF0
			public virtual MultiValuedProperty<string> BlockedFileTypes
			{
				set
				{
					base.PowerSharpParameters["BlockedFileTypes"] = value;
				}
			}

			// Token: 0x17005324 RID: 21284
			// (set) Token: 0x06007A8D RID: 31373 RVA: 0x000B6C03 File Offset: 0x000B4E03
			public virtual MultiValuedProperty<string> BlockedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["BlockedMimeTypes"] = value;
				}
			}

			// Token: 0x17005325 RID: 21285
			// (set) Token: 0x06007A8E RID: 31374 RVA: 0x000B6C16 File Offset: 0x000B4E16
			public virtual MultiValuedProperty<string> RemoteDocumentsAllowedServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsAllowedServers"] = value;
				}
			}

			// Token: 0x17005326 RID: 21286
			// (set) Token: 0x06007A8F RID: 31375 RVA: 0x000B6C29 File Offset: 0x000B4E29
			public virtual MultiValuedProperty<string> RemoteDocumentsBlockedServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsBlockedServers"] = value;
				}
			}

			// Token: 0x17005327 RID: 21287
			// (set) Token: 0x06007A90 RID: 31376 RVA: 0x000B6C3C File Offset: 0x000B4E3C
			public virtual MultiValuedProperty<string> RemoteDocumentsInternalDomainSuffixList
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsInternalDomainSuffixList"] = value;
				}
			}

			// Token: 0x17005328 RID: 21288
			// (set) Token: 0x06007A91 RID: 31377 RVA: 0x000B6C4F File Offset: 0x000B4E4F
			public virtual LogonFormats LogonFormat
			{
				set
				{
					base.PowerSharpParameters["LogonFormat"] = value;
				}
			}

			// Token: 0x17005329 RID: 21289
			// (set) Token: 0x06007A92 RID: 31378 RVA: 0x000B6C67 File Offset: 0x000B4E67
			public virtual ClientAuthCleanupLevels ClientAuthCleanupLevel
			{
				set
				{
					base.PowerSharpParameters["ClientAuthCleanupLevel"] = value;
				}
			}

			// Token: 0x1700532A RID: 21290
			// (set) Token: 0x06007A93 RID: 31379 RVA: 0x000B6C7F File Offset: 0x000B4E7F
			public virtual bool? LogonPagePublicPrivateSelectionEnabled
			{
				set
				{
					base.PowerSharpParameters["LogonPagePublicPrivateSelectionEnabled"] = value;
				}
			}

			// Token: 0x1700532B RID: 21291
			// (set) Token: 0x06007A94 RID: 31380 RVA: 0x000B6C97 File Offset: 0x000B4E97
			public virtual bool? LogonPageLightSelectionEnabled
			{
				set
				{
					base.PowerSharpParameters["LogonPageLightSelectionEnabled"] = value;
				}
			}

			// Token: 0x1700532C RID: 21292
			// (set) Token: 0x06007A95 RID: 31381 RVA: 0x000B6CAF File Offset: 0x000B4EAF
			public virtual bool? IsPublic
			{
				set
				{
					base.PowerSharpParameters["IsPublic"] = value;
				}
			}

			// Token: 0x1700532D RID: 21293
			// (set) Token: 0x06007A96 RID: 31382 RVA: 0x000B6CC7 File Offset: 0x000B4EC7
			public virtual WebBeaconFilterLevels? FilterWebBeaconsAndHtmlForms
			{
				set
				{
					base.PowerSharpParameters["FilterWebBeaconsAndHtmlForms"] = value;
				}
			}

			// Token: 0x1700532E RID: 21294
			// (set) Token: 0x06007A97 RID: 31383 RVA: 0x000B6CDF File Offset: 0x000B4EDF
			public virtual int? NotificationInterval
			{
				set
				{
					base.PowerSharpParameters["NotificationInterval"] = value;
				}
			}

			// Token: 0x1700532F RID: 21295
			// (set) Token: 0x06007A98 RID: 31384 RVA: 0x000B6CF7 File Offset: 0x000B4EF7
			public virtual string DefaultTheme
			{
				set
				{
					base.PowerSharpParameters["DefaultTheme"] = value;
				}
			}

			// Token: 0x17005330 RID: 21296
			// (set) Token: 0x06007A99 RID: 31385 RVA: 0x000B6D0A File Offset: 0x000B4F0A
			public virtual int? UserContextTimeout
			{
				set
				{
					base.PowerSharpParameters["UserContextTimeout"] = value;
				}
			}

			// Token: 0x17005331 RID: 21297
			// (set) Token: 0x06007A9A RID: 31386 RVA: 0x000B6D22 File Offset: 0x000B4F22
			public virtual ExchwebProxyDestinations? ExchwebProxyDestination
			{
				set
				{
					base.PowerSharpParameters["ExchwebProxyDestination"] = value;
				}
			}

			// Token: 0x17005332 RID: 21298
			// (set) Token: 0x06007A9B RID: 31387 RVA: 0x000B6D3A File Offset: 0x000B4F3A
			public virtual VirtualDirectoryTypes? VirtualDirectoryType
			{
				set
				{
					base.PowerSharpParameters["VirtualDirectoryType"] = value;
				}
			}

			// Token: 0x17005333 RID: 21299
			// (set) Token: 0x06007A9C RID: 31388 RVA: 0x000B6D52 File Offset: 0x000B4F52
			public virtual string InstantMessagingCertificateThumbprint
			{
				set
				{
					base.PowerSharpParameters["InstantMessagingCertificateThumbprint"] = value;
				}
			}

			// Token: 0x17005334 RID: 21300
			// (set) Token: 0x06007A9D RID: 31389 RVA: 0x000B6D65 File Offset: 0x000B4F65
			public virtual string InstantMessagingServerName
			{
				set
				{
					base.PowerSharpParameters["InstantMessagingServerName"] = value;
				}
			}

			// Token: 0x17005335 RID: 21301
			// (set) Token: 0x06007A9E RID: 31390 RVA: 0x000B6D78 File Offset: 0x000B4F78
			public virtual bool? RedirectToOptimalOWAServer
			{
				set
				{
					base.PowerSharpParameters["RedirectToOptimalOWAServer"] = value;
				}
			}

			// Token: 0x17005336 RID: 21302
			// (set) Token: 0x06007A9F RID: 31391 RVA: 0x000B6D90 File Offset: 0x000B4F90
			public virtual int? DefaultClientLanguage
			{
				set
				{
					base.PowerSharpParameters["DefaultClientLanguage"] = value;
				}
			}

			// Token: 0x17005337 RID: 21303
			// (set) Token: 0x06007AA0 RID: 31392 RVA: 0x000B6DA8 File Offset: 0x000B4FA8
			public virtual int LogonAndErrorLanguage
			{
				set
				{
					base.PowerSharpParameters["LogonAndErrorLanguage"] = value;
				}
			}

			// Token: 0x17005338 RID: 21304
			// (set) Token: 0x06007AA1 RID: 31393 RVA: 0x000B6DC0 File Offset: 0x000B4FC0
			public virtual bool? UseGB18030
			{
				set
				{
					base.PowerSharpParameters["UseGB18030"] = value;
				}
			}

			// Token: 0x17005339 RID: 21305
			// (set) Token: 0x06007AA2 RID: 31394 RVA: 0x000B6DD8 File Offset: 0x000B4FD8
			public virtual bool? UseISO885915
			{
				set
				{
					base.PowerSharpParameters["UseISO885915"] = value;
				}
			}

			// Token: 0x1700533A RID: 21306
			// (set) Token: 0x06007AA3 RID: 31395 RVA: 0x000B6DF0 File Offset: 0x000B4FF0
			public virtual OutboundCharsetOptions? OutboundCharset
			{
				set
				{
					base.PowerSharpParameters["OutboundCharset"] = value;
				}
			}

			// Token: 0x1700533B RID: 21307
			// (set) Token: 0x06007AA4 RID: 31396 RVA: 0x000B6E08 File Offset: 0x000B5008
			public virtual bool? GlobalAddressListEnabled
			{
				set
				{
					base.PowerSharpParameters["GlobalAddressListEnabled"] = value;
				}
			}

			// Token: 0x1700533C RID: 21308
			// (set) Token: 0x06007AA5 RID: 31397 RVA: 0x000B6E20 File Offset: 0x000B5020
			public virtual bool? OrganizationEnabled
			{
				set
				{
					base.PowerSharpParameters["OrganizationEnabled"] = value;
				}
			}

			// Token: 0x1700533D RID: 21309
			// (set) Token: 0x06007AA6 RID: 31398 RVA: 0x000B6E38 File Offset: 0x000B5038
			public virtual bool? ExplicitLogonEnabled
			{
				set
				{
					base.PowerSharpParameters["ExplicitLogonEnabled"] = value;
				}
			}

			// Token: 0x1700533E RID: 21310
			// (set) Token: 0x06007AA7 RID: 31399 RVA: 0x000B6E50 File Offset: 0x000B5050
			public virtual bool? OWALightEnabled
			{
				set
				{
					base.PowerSharpParameters["OWALightEnabled"] = value;
				}
			}

			// Token: 0x1700533F RID: 21311
			// (set) Token: 0x06007AA8 RID: 31400 RVA: 0x000B6E68 File Offset: 0x000B5068
			public virtual bool? DelegateAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["DelegateAccessEnabled"] = value;
				}
			}

			// Token: 0x17005340 RID: 21312
			// (set) Token: 0x06007AA9 RID: 31401 RVA: 0x000B6E80 File Offset: 0x000B5080
			public virtual bool? IRMEnabled
			{
				set
				{
					base.PowerSharpParameters["IRMEnabled"] = value;
				}
			}

			// Token: 0x17005341 RID: 21313
			// (set) Token: 0x06007AAA RID: 31402 RVA: 0x000B6E98 File Offset: 0x000B5098
			public virtual bool? CalendarEnabled
			{
				set
				{
					base.PowerSharpParameters["CalendarEnabled"] = value;
				}
			}

			// Token: 0x17005342 RID: 21314
			// (set) Token: 0x06007AAB RID: 31403 RVA: 0x000B6EB0 File Offset: 0x000B50B0
			public virtual bool? ContactsEnabled
			{
				set
				{
					base.PowerSharpParameters["ContactsEnabled"] = value;
				}
			}

			// Token: 0x17005343 RID: 21315
			// (set) Token: 0x06007AAC RID: 31404 RVA: 0x000B6EC8 File Offset: 0x000B50C8
			public virtual bool? TasksEnabled
			{
				set
				{
					base.PowerSharpParameters["TasksEnabled"] = value;
				}
			}

			// Token: 0x17005344 RID: 21316
			// (set) Token: 0x06007AAD RID: 31405 RVA: 0x000B6EE0 File Offset: 0x000B50E0
			public virtual bool? JournalEnabled
			{
				set
				{
					base.PowerSharpParameters["JournalEnabled"] = value;
				}
			}

			// Token: 0x17005345 RID: 21317
			// (set) Token: 0x06007AAE RID: 31406 RVA: 0x000B6EF8 File Offset: 0x000B50F8
			public virtual bool? NotesEnabled
			{
				set
				{
					base.PowerSharpParameters["NotesEnabled"] = value;
				}
			}

			// Token: 0x17005346 RID: 21318
			// (set) Token: 0x06007AAF RID: 31407 RVA: 0x000B6F10 File Offset: 0x000B5110
			public virtual bool? RemindersAndNotificationsEnabled
			{
				set
				{
					base.PowerSharpParameters["RemindersAndNotificationsEnabled"] = value;
				}
			}

			// Token: 0x17005347 RID: 21319
			// (set) Token: 0x06007AB0 RID: 31408 RVA: 0x000B6F28 File Offset: 0x000B5128
			public virtual bool? PremiumClientEnabled
			{
				set
				{
					base.PowerSharpParameters["PremiumClientEnabled"] = value;
				}
			}

			// Token: 0x17005348 RID: 21320
			// (set) Token: 0x06007AB1 RID: 31409 RVA: 0x000B6F40 File Offset: 0x000B5140
			public virtual bool? SpellCheckerEnabled
			{
				set
				{
					base.PowerSharpParameters["SpellCheckerEnabled"] = value;
				}
			}

			// Token: 0x17005349 RID: 21321
			// (set) Token: 0x06007AB2 RID: 31410 RVA: 0x000B6F58 File Offset: 0x000B5158
			public virtual bool? SearchFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["SearchFoldersEnabled"] = value;
				}
			}

			// Token: 0x1700534A RID: 21322
			// (set) Token: 0x06007AB3 RID: 31411 RVA: 0x000B6F70 File Offset: 0x000B5170
			public virtual bool? SignaturesEnabled
			{
				set
				{
					base.PowerSharpParameters["SignaturesEnabled"] = value;
				}
			}

			// Token: 0x1700534B RID: 21323
			// (set) Token: 0x06007AB4 RID: 31412 RVA: 0x000B6F88 File Offset: 0x000B5188
			public virtual bool? ThemeSelectionEnabled
			{
				set
				{
					base.PowerSharpParameters["ThemeSelectionEnabled"] = value;
				}
			}

			// Token: 0x1700534C RID: 21324
			// (set) Token: 0x06007AB5 RID: 31413 RVA: 0x000B6FA0 File Offset: 0x000B51A0
			public virtual bool? JunkEmailEnabled
			{
				set
				{
					base.PowerSharpParameters["JunkEmailEnabled"] = value;
				}
			}

			// Token: 0x1700534D RID: 21325
			// (set) Token: 0x06007AB6 RID: 31414 RVA: 0x000B6FB8 File Offset: 0x000B51B8
			public virtual bool? UMIntegrationEnabled
			{
				set
				{
					base.PowerSharpParameters["UMIntegrationEnabled"] = value;
				}
			}

			// Token: 0x1700534E RID: 21326
			// (set) Token: 0x06007AB7 RID: 31415 RVA: 0x000B6FD0 File Offset: 0x000B51D0
			public virtual bool? WSSAccessOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WSSAccessOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x1700534F RID: 21327
			// (set) Token: 0x06007AB8 RID: 31416 RVA: 0x000B6FE8 File Offset: 0x000B51E8
			public virtual bool? WSSAccessOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WSSAccessOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17005350 RID: 21328
			// (set) Token: 0x06007AB9 RID: 31417 RVA: 0x000B7000 File Offset: 0x000B5200
			public virtual bool? ChangePasswordEnabled
			{
				set
				{
					base.PowerSharpParameters["ChangePasswordEnabled"] = value;
				}
			}

			// Token: 0x17005351 RID: 21329
			// (set) Token: 0x06007ABA RID: 31418 RVA: 0x000B7018 File Offset: 0x000B5218
			public virtual bool? UNCAccessOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["UNCAccessOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x17005352 RID: 21330
			// (set) Token: 0x06007ABB RID: 31419 RVA: 0x000B7030 File Offset: 0x000B5230
			public virtual bool? UNCAccessOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["UNCAccessOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17005353 RID: 21331
			// (set) Token: 0x06007ABC RID: 31420 RVA: 0x000B7048 File Offset: 0x000B5248
			public virtual bool? ActiveSyncIntegrationEnabled
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncIntegrationEnabled"] = value;
				}
			}

			// Token: 0x17005354 RID: 21332
			// (set) Token: 0x06007ABD RID: 31421 RVA: 0x000B7060 File Offset: 0x000B5260
			public virtual bool? AllAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["AllAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17005355 RID: 21333
			// (set) Token: 0x06007ABE RID: 31422 RVA: 0x000B7078 File Offset: 0x000B5278
			public virtual bool? RulesEnabled
			{
				set
				{
					base.PowerSharpParameters["RulesEnabled"] = value;
				}
			}

			// Token: 0x17005356 RID: 21334
			// (set) Token: 0x06007ABF RID: 31423 RVA: 0x000B7090 File Offset: 0x000B5290
			public virtual bool? PublicFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["PublicFoldersEnabled"] = value;
				}
			}

			// Token: 0x17005357 RID: 21335
			// (set) Token: 0x06007AC0 RID: 31424 RVA: 0x000B70A8 File Offset: 0x000B52A8
			public virtual bool? SMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["SMimeEnabled"] = value;
				}
			}

			// Token: 0x17005358 RID: 21336
			// (set) Token: 0x06007AC1 RID: 31425 RVA: 0x000B70C0 File Offset: 0x000B52C0
			public virtual bool? RecoverDeletedItemsEnabled
			{
				set
				{
					base.PowerSharpParameters["RecoverDeletedItemsEnabled"] = value;
				}
			}

			// Token: 0x17005359 RID: 21337
			// (set) Token: 0x06007AC2 RID: 31426 RVA: 0x000B70D8 File Offset: 0x000B52D8
			public virtual bool? InstantMessagingEnabled
			{
				set
				{
					base.PowerSharpParameters["InstantMessagingEnabled"] = value;
				}
			}

			// Token: 0x1700535A RID: 21338
			// (set) Token: 0x06007AC3 RID: 31427 RVA: 0x000B70F0 File Offset: 0x000B52F0
			public virtual bool? TextMessagingEnabled
			{
				set
				{
					base.PowerSharpParameters["TextMessagingEnabled"] = value;
				}
			}

			// Token: 0x1700535B RID: 21339
			// (set) Token: 0x06007AC4 RID: 31428 RVA: 0x000B7108 File Offset: 0x000B5308
			public virtual bool? ForceSaveAttachmentFilteringEnabled
			{
				set
				{
					base.PowerSharpParameters["ForceSaveAttachmentFilteringEnabled"] = value;
				}
			}

			// Token: 0x1700535C RID: 21340
			// (set) Token: 0x06007AC5 RID: 31429 RVA: 0x000B7120 File Offset: 0x000B5320
			public virtual bool? SilverlightEnabled
			{
				set
				{
					base.PowerSharpParameters["SilverlightEnabled"] = value;
				}
			}

			// Token: 0x1700535D RID: 21341
			// (set) Token: 0x06007AC6 RID: 31430 RVA: 0x000B7138 File Offset: 0x000B5338
			public virtual bool? PlacesEnabled
			{
				set
				{
					base.PowerSharpParameters["PlacesEnabled"] = value;
				}
			}

			// Token: 0x1700535E RID: 21342
			// (set) Token: 0x06007AC7 RID: 31431 RVA: 0x000B7150 File Offset: 0x000B5350
			public virtual bool? WeatherEnabled
			{
				set
				{
					base.PowerSharpParameters["WeatherEnabled"] = value;
				}
			}

			// Token: 0x1700535F RID: 21343
			// (set) Token: 0x06007AC8 RID: 31432 RVA: 0x000B7168 File Offset: 0x000B5368
			public virtual bool? AllowCopyContactsToDeviceAddressBook
			{
				set
				{
					base.PowerSharpParameters["AllowCopyContactsToDeviceAddressBook"] = value;
				}
			}

			// Token: 0x17005360 RID: 21344
			// (set) Token: 0x06007AC9 RID: 31433 RVA: 0x000B7180 File Offset: 0x000B5380
			public virtual bool? AnonymousFeaturesEnabled
			{
				set
				{
					base.PowerSharpParameters["AnonymousFeaturesEnabled"] = value;
				}
			}

			// Token: 0x17005361 RID: 21345
			// (set) Token: 0x06007ACA RID: 31434 RVA: 0x000B7198 File Offset: 0x000B5398
			public virtual bool? IntegratedFeaturesEnabled
			{
				set
				{
					base.PowerSharpParameters["IntegratedFeaturesEnabled"] = value;
				}
			}

			// Token: 0x17005362 RID: 21346
			// (set) Token: 0x06007ACB RID: 31435 RVA: 0x000B71B0 File Offset: 0x000B53B0
			public virtual bool? DisplayPhotosEnabled
			{
				set
				{
					base.PowerSharpParameters["DisplayPhotosEnabled"] = value;
				}
			}

			// Token: 0x17005363 RID: 21347
			// (set) Token: 0x06007ACC RID: 31436 RVA: 0x000B71C8 File Offset: 0x000B53C8
			public virtual bool? SetPhotoEnabled
			{
				set
				{
					base.PowerSharpParameters["SetPhotoEnabled"] = value;
				}
			}

			// Token: 0x17005364 RID: 21348
			// (set) Token: 0x06007ACD RID: 31437 RVA: 0x000B71E0 File Offset: 0x000B53E0
			public virtual bool? PredictedActionsEnabled
			{
				set
				{
					base.PowerSharpParameters["PredictedActionsEnabled"] = value;
				}
			}

			// Token: 0x17005365 RID: 21349
			// (set) Token: 0x06007ACE RID: 31438 RVA: 0x000B71F8 File Offset: 0x000B53F8
			public virtual bool? UserDiagnosticEnabled
			{
				set
				{
					base.PowerSharpParameters["UserDiagnosticEnabled"] = value;
				}
			}

			// Token: 0x17005366 RID: 21350
			// (set) Token: 0x06007ACF RID: 31439 RVA: 0x000B7210 File Offset: 0x000B5410
			public virtual bool? ReportJunkEmailEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportJunkEmailEnabled"] = value;
				}
			}

			// Token: 0x17005367 RID: 21351
			// (set) Token: 0x06007AD0 RID: 31440 RVA: 0x000B7228 File Offset: 0x000B5428
			public virtual WebPartsFrameOptions? WebPartsFrameOptionsType
			{
				set
				{
					base.PowerSharpParameters["WebPartsFrameOptionsType"] = value;
				}
			}

			// Token: 0x17005368 RID: 21352
			// (set) Token: 0x06007AD1 RID: 31441 RVA: 0x000B7240 File Offset: 0x000B5440
			public virtual AllowOfflineOnEnum AllowOfflineOn
			{
				set
				{
					base.PowerSharpParameters["AllowOfflineOn"] = value;
				}
			}

			// Token: 0x17005369 RID: 21353
			// (set) Token: 0x06007AD2 RID: 31442 RVA: 0x000B7258 File Offset: 0x000B5458
			public virtual string SetPhotoURL
			{
				set
				{
					base.PowerSharpParameters["SetPhotoURL"] = value;
				}
			}

			// Token: 0x1700536A RID: 21354
			// (set) Token: 0x06007AD3 RID: 31443 RVA: 0x000B726B File Offset: 0x000B546B
			public virtual InstantMessagingTypeOptions? InstantMessagingType
			{
				set
				{
					base.PowerSharpParameters["InstantMessagingType"] = value;
				}
			}

			// Token: 0x1700536B RID: 21355
			// (set) Token: 0x06007AD4 RID: 31444 RVA: 0x000B7283 File Offset: 0x000B5483
			public virtual Uri Exchange2003Url
			{
				set
				{
					base.PowerSharpParameters["Exchange2003Url"] = value;
				}
			}

			// Token: 0x1700536C RID: 21356
			// (set) Token: 0x06007AD5 RID: 31445 RVA: 0x000B7296 File Offset: 0x000B5496
			public virtual Uri FailbackUrl
			{
				set
				{
					base.PowerSharpParameters["FailbackUrl"] = value;
				}
			}

			// Token: 0x1700536D RID: 21357
			// (set) Token: 0x06007AD6 RID: 31446 RVA: 0x000B72A9 File Offset: 0x000B54A9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700536E RID: 21358
			// (set) Token: 0x06007AD7 RID: 31447 RVA: 0x000B72C1 File Offset: 0x000B54C1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700536F RID: 21359
			// (set) Token: 0x06007AD8 RID: 31448 RVA: 0x000B72D9 File Offset: 0x000B54D9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005370 RID: 21360
			// (set) Token: 0x06007AD9 RID: 31449 RVA: 0x000B72F1 File Offset: 0x000B54F1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005371 RID: 21361
			// (set) Token: 0x06007ADA RID: 31450 RVA: 0x000B7309 File Offset: 0x000B5509
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000991 RID: 2449
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005372 RID: 21362
			// (set) Token: 0x06007ADC RID: 31452 RVA: 0x000B7329 File Offset: 0x000B5529
			public virtual VirtualDirectoryIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17005373 RID: 21363
			// (set) Token: 0x06007ADD RID: 31453 RVA: 0x000B733C File Offset: 0x000B553C
			public virtual string DefaultDomain
			{
				set
				{
					base.PowerSharpParameters["DefaultDomain"] = value;
				}
			}

			// Token: 0x17005374 RID: 21364
			// (set) Token: 0x06007ADE RID: 31454 RVA: 0x000B734F File Offset: 0x000B554F
			public virtual bool DigestAuthentication
			{
				set
				{
					base.PowerSharpParameters["DigestAuthentication"] = value;
				}
			}

			// Token: 0x17005375 RID: 21365
			// (set) Token: 0x06007ADF RID: 31455 RVA: 0x000B7367 File Offset: 0x000B5567
			public virtual bool FormsAuthentication
			{
				set
				{
					base.PowerSharpParameters["FormsAuthentication"] = value;
				}
			}

			// Token: 0x17005376 RID: 21366
			// (set) Token: 0x06007AE0 RID: 31456 RVA: 0x000B737F File Offset: 0x000B557F
			public virtual bool OAuthAuthentication
			{
				set
				{
					base.PowerSharpParameters["OAuthAuthentication"] = value;
				}
			}

			// Token: 0x17005377 RID: 21367
			// (set) Token: 0x06007AE1 RID: 31457 RVA: 0x000B7397 File Offset: 0x000B5597
			public virtual bool AdfsAuthentication
			{
				set
				{
					base.PowerSharpParameters["AdfsAuthentication"] = value;
				}
			}

			// Token: 0x17005378 RID: 21368
			// (set) Token: 0x06007AE2 RID: 31458 RVA: 0x000B73AF File Offset: 0x000B55AF
			public virtual bool BasicAuthentication
			{
				set
				{
					base.PowerSharpParameters["BasicAuthentication"] = value;
				}
			}

			// Token: 0x17005379 RID: 21369
			// (set) Token: 0x06007AE3 RID: 31459 RVA: 0x000B73C7 File Offset: 0x000B55C7
			public virtual bool WindowsAuthentication
			{
				set
				{
					base.PowerSharpParameters["WindowsAuthentication"] = value;
				}
			}

			// Token: 0x1700537A RID: 21370
			// (set) Token: 0x06007AE4 RID: 31460 RVA: 0x000B73DF File Offset: 0x000B55DF
			public virtual bool LiveIdAuthentication
			{
				set
				{
					base.PowerSharpParameters["LiveIdAuthentication"] = value;
				}
			}

			// Token: 0x1700537B RID: 21371
			// (set) Token: 0x06007AE5 RID: 31461 RVA: 0x000B73F7 File Offset: 0x000B55F7
			public virtual GzipLevel GzipLevel
			{
				set
				{
					base.PowerSharpParameters["GzipLevel"] = value;
				}
			}

			// Token: 0x1700537C RID: 21372
			// (set) Token: 0x06007AE6 RID: 31462 RVA: 0x000B740F File Offset: 0x000B560F
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionTokenChecking"] = value;
				}
			}

			// Token: 0x1700537D RID: 21373
			// (set) Token: 0x06007AE7 RID: 31463 RVA: 0x000B7427 File Offset: 0x000B5627
			public virtual MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionFlags"] = value;
				}
			}

			// Token: 0x1700537E RID: 21374
			// (set) Token: 0x06007AE8 RID: 31464 RVA: 0x000B743A File Offset: 0x000B563A
			public virtual MultiValuedProperty<string> ExtendedProtectionSPNList
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionSPNList"] = value;
				}
			}

			// Token: 0x1700537F RID: 21375
			// (set) Token: 0x06007AE9 RID: 31465 RVA: 0x000B744D File Offset: 0x000B564D
			public virtual Uri InternalUrl
			{
				set
				{
					base.PowerSharpParameters["InternalUrl"] = value;
				}
			}

			// Token: 0x17005380 RID: 21376
			// (set) Token: 0x06007AEA RID: 31466 RVA: 0x000B7460 File Offset: 0x000B5660
			public virtual Uri ExternalUrl
			{
				set
				{
					base.PowerSharpParameters["ExternalUrl"] = value;
				}
			}

			// Token: 0x17005381 RID: 21377
			// (set) Token: 0x06007AEB RID: 31467 RVA: 0x000B7473 File Offset: 0x000B5673
			public virtual MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
			{
				set
				{
					base.PowerSharpParameters["ExternalAuthenticationMethods"] = value;
				}
			}

			// Token: 0x17005382 RID: 21378
			// (set) Token: 0x06007AEC RID: 31468 RVA: 0x000B7486 File Offset: 0x000B5686
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005383 RID: 21379
			// (set) Token: 0x06007AED RID: 31469 RVA: 0x000B7499 File Offset: 0x000B5699
			public virtual bool? DirectFileAccessOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["DirectFileAccessOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x17005384 RID: 21380
			// (set) Token: 0x06007AEE RID: 31470 RVA: 0x000B74B1 File Offset: 0x000B56B1
			public virtual bool? DirectFileAccessOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["DirectFileAccessOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17005385 RID: 21381
			// (set) Token: 0x06007AEF RID: 31471 RVA: 0x000B74C9 File Offset: 0x000B56C9
			public virtual bool? WebReadyDocumentViewingOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x17005386 RID: 21382
			// (set) Token: 0x06007AF0 RID: 31472 RVA: 0x000B74E1 File Offset: 0x000B56E1
			public virtual bool? WebReadyDocumentViewingOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x17005387 RID: 21383
			// (set) Token: 0x06007AF1 RID: 31473 RVA: 0x000B74F9 File Offset: 0x000B56F9
			public virtual bool? ForceWebReadyDocumentViewingFirstOnPublicComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWebReadyDocumentViewingFirstOnPublicComputers"] = value;
				}
			}

			// Token: 0x17005388 RID: 21384
			// (set) Token: 0x06007AF2 RID: 31474 RVA: 0x000B7511 File Offset: 0x000B5711
			public virtual bool? ForceWebReadyDocumentViewingFirstOnPrivateComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWebReadyDocumentViewingFirstOnPrivateComputers"] = value;
				}
			}

			// Token: 0x17005389 RID: 21385
			// (set) Token: 0x06007AF3 RID: 31475 RVA: 0x000B7529 File Offset: 0x000B5729
			public virtual bool? WacViewingOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WacViewingOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x1700538A RID: 21386
			// (set) Token: 0x06007AF4 RID: 31476 RVA: 0x000B7541 File Offset: 0x000B5741
			public virtual bool? WacViewingOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WacViewingOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x1700538B RID: 21387
			// (set) Token: 0x06007AF5 RID: 31477 RVA: 0x000B7559 File Offset: 0x000B5759
			public virtual bool? ForceWacViewingFirstOnPublicComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWacViewingFirstOnPublicComputers"] = value;
				}
			}

			// Token: 0x1700538C RID: 21388
			// (set) Token: 0x06007AF6 RID: 31478 RVA: 0x000B7571 File Offset: 0x000B5771
			public virtual bool? ForceWacViewingFirstOnPrivateComputers
			{
				set
				{
					base.PowerSharpParameters["ForceWacViewingFirstOnPrivateComputers"] = value;
				}
			}

			// Token: 0x1700538D RID: 21389
			// (set) Token: 0x06007AF7 RID: 31479 RVA: 0x000B7589 File Offset: 0x000B5789
			public virtual RemoteDocumentsActions? RemoteDocumentsActionForUnknownServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsActionForUnknownServers"] = value;
				}
			}

			// Token: 0x1700538E RID: 21390
			// (set) Token: 0x06007AF8 RID: 31480 RVA: 0x000B75A1 File Offset: 0x000B57A1
			public virtual AttachmentBlockingActions? ActionForUnknownFileAndMIMETypes
			{
				set
				{
					base.PowerSharpParameters["ActionForUnknownFileAndMIMETypes"] = value;
				}
			}

			// Token: 0x1700538F RID: 21391
			// (set) Token: 0x06007AF9 RID: 31481 RVA: 0x000B75B9 File Offset: 0x000B57B9
			public virtual MultiValuedProperty<string> WebReadyFileTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyFileTypes"] = value;
				}
			}

			// Token: 0x17005390 RID: 21392
			// (set) Token: 0x06007AFA RID: 31482 RVA: 0x000B75CC File Offset: 0x000B57CC
			public virtual MultiValuedProperty<string> WebReadyMimeTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyMimeTypes"] = value;
				}
			}

			// Token: 0x17005391 RID: 21393
			// (set) Token: 0x06007AFB RID: 31483 RVA: 0x000B75DF File Offset: 0x000B57DF
			public virtual bool? WebReadyDocumentViewingForAllSupportedTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingForAllSupportedTypes"] = value;
				}
			}

			// Token: 0x17005392 RID: 21394
			// (set) Token: 0x06007AFC RID: 31484 RVA: 0x000B75F7 File Offset: 0x000B57F7
			public virtual MultiValuedProperty<string> WebReadyDocumentViewingSupportedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingSupportedMimeTypes"] = value;
				}
			}

			// Token: 0x17005393 RID: 21395
			// (set) Token: 0x06007AFD RID: 31485 RVA: 0x000B760A File Offset: 0x000B580A
			public virtual MultiValuedProperty<string> WebReadyDocumentViewingSupportedFileTypes
			{
				set
				{
					base.PowerSharpParameters["WebReadyDocumentViewingSupportedFileTypes"] = value;
				}
			}

			// Token: 0x17005394 RID: 21396
			// (set) Token: 0x06007AFE RID: 31486 RVA: 0x000B761D File Offset: 0x000B581D
			public virtual MultiValuedProperty<string> AllowedFileTypes
			{
				set
				{
					base.PowerSharpParameters["AllowedFileTypes"] = value;
				}
			}

			// Token: 0x17005395 RID: 21397
			// (set) Token: 0x06007AFF RID: 31487 RVA: 0x000B7630 File Offset: 0x000B5830
			public virtual MultiValuedProperty<string> AllowedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["AllowedMimeTypes"] = value;
				}
			}

			// Token: 0x17005396 RID: 21398
			// (set) Token: 0x06007B00 RID: 31488 RVA: 0x000B7643 File Offset: 0x000B5843
			public virtual MultiValuedProperty<string> ForceSaveFileTypes
			{
				set
				{
					base.PowerSharpParameters["ForceSaveFileTypes"] = value;
				}
			}

			// Token: 0x17005397 RID: 21399
			// (set) Token: 0x06007B01 RID: 31489 RVA: 0x000B7656 File Offset: 0x000B5856
			public virtual MultiValuedProperty<string> ForceSaveMimeTypes
			{
				set
				{
					base.PowerSharpParameters["ForceSaveMimeTypes"] = value;
				}
			}

			// Token: 0x17005398 RID: 21400
			// (set) Token: 0x06007B02 RID: 31490 RVA: 0x000B7669 File Offset: 0x000B5869
			public virtual MultiValuedProperty<string> BlockedFileTypes
			{
				set
				{
					base.PowerSharpParameters["BlockedFileTypes"] = value;
				}
			}

			// Token: 0x17005399 RID: 21401
			// (set) Token: 0x06007B03 RID: 31491 RVA: 0x000B767C File Offset: 0x000B587C
			public virtual MultiValuedProperty<string> BlockedMimeTypes
			{
				set
				{
					base.PowerSharpParameters["BlockedMimeTypes"] = value;
				}
			}

			// Token: 0x1700539A RID: 21402
			// (set) Token: 0x06007B04 RID: 31492 RVA: 0x000B768F File Offset: 0x000B588F
			public virtual MultiValuedProperty<string> RemoteDocumentsAllowedServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsAllowedServers"] = value;
				}
			}

			// Token: 0x1700539B RID: 21403
			// (set) Token: 0x06007B05 RID: 31493 RVA: 0x000B76A2 File Offset: 0x000B58A2
			public virtual MultiValuedProperty<string> RemoteDocumentsBlockedServers
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsBlockedServers"] = value;
				}
			}

			// Token: 0x1700539C RID: 21404
			// (set) Token: 0x06007B06 RID: 31494 RVA: 0x000B76B5 File Offset: 0x000B58B5
			public virtual MultiValuedProperty<string> RemoteDocumentsInternalDomainSuffixList
			{
				set
				{
					base.PowerSharpParameters["RemoteDocumentsInternalDomainSuffixList"] = value;
				}
			}

			// Token: 0x1700539D RID: 21405
			// (set) Token: 0x06007B07 RID: 31495 RVA: 0x000B76C8 File Offset: 0x000B58C8
			public virtual LogonFormats LogonFormat
			{
				set
				{
					base.PowerSharpParameters["LogonFormat"] = value;
				}
			}

			// Token: 0x1700539E RID: 21406
			// (set) Token: 0x06007B08 RID: 31496 RVA: 0x000B76E0 File Offset: 0x000B58E0
			public virtual ClientAuthCleanupLevels ClientAuthCleanupLevel
			{
				set
				{
					base.PowerSharpParameters["ClientAuthCleanupLevel"] = value;
				}
			}

			// Token: 0x1700539F RID: 21407
			// (set) Token: 0x06007B09 RID: 31497 RVA: 0x000B76F8 File Offset: 0x000B58F8
			public virtual bool? LogonPagePublicPrivateSelectionEnabled
			{
				set
				{
					base.PowerSharpParameters["LogonPagePublicPrivateSelectionEnabled"] = value;
				}
			}

			// Token: 0x170053A0 RID: 21408
			// (set) Token: 0x06007B0A RID: 31498 RVA: 0x000B7710 File Offset: 0x000B5910
			public virtual bool? LogonPageLightSelectionEnabled
			{
				set
				{
					base.PowerSharpParameters["LogonPageLightSelectionEnabled"] = value;
				}
			}

			// Token: 0x170053A1 RID: 21409
			// (set) Token: 0x06007B0B RID: 31499 RVA: 0x000B7728 File Offset: 0x000B5928
			public virtual bool? IsPublic
			{
				set
				{
					base.PowerSharpParameters["IsPublic"] = value;
				}
			}

			// Token: 0x170053A2 RID: 21410
			// (set) Token: 0x06007B0C RID: 31500 RVA: 0x000B7740 File Offset: 0x000B5940
			public virtual WebBeaconFilterLevels? FilterWebBeaconsAndHtmlForms
			{
				set
				{
					base.PowerSharpParameters["FilterWebBeaconsAndHtmlForms"] = value;
				}
			}

			// Token: 0x170053A3 RID: 21411
			// (set) Token: 0x06007B0D RID: 31501 RVA: 0x000B7758 File Offset: 0x000B5958
			public virtual int? NotificationInterval
			{
				set
				{
					base.PowerSharpParameters["NotificationInterval"] = value;
				}
			}

			// Token: 0x170053A4 RID: 21412
			// (set) Token: 0x06007B0E RID: 31502 RVA: 0x000B7770 File Offset: 0x000B5970
			public virtual string DefaultTheme
			{
				set
				{
					base.PowerSharpParameters["DefaultTheme"] = value;
				}
			}

			// Token: 0x170053A5 RID: 21413
			// (set) Token: 0x06007B0F RID: 31503 RVA: 0x000B7783 File Offset: 0x000B5983
			public virtual int? UserContextTimeout
			{
				set
				{
					base.PowerSharpParameters["UserContextTimeout"] = value;
				}
			}

			// Token: 0x170053A6 RID: 21414
			// (set) Token: 0x06007B10 RID: 31504 RVA: 0x000B779B File Offset: 0x000B599B
			public virtual ExchwebProxyDestinations? ExchwebProxyDestination
			{
				set
				{
					base.PowerSharpParameters["ExchwebProxyDestination"] = value;
				}
			}

			// Token: 0x170053A7 RID: 21415
			// (set) Token: 0x06007B11 RID: 31505 RVA: 0x000B77B3 File Offset: 0x000B59B3
			public virtual VirtualDirectoryTypes? VirtualDirectoryType
			{
				set
				{
					base.PowerSharpParameters["VirtualDirectoryType"] = value;
				}
			}

			// Token: 0x170053A8 RID: 21416
			// (set) Token: 0x06007B12 RID: 31506 RVA: 0x000B77CB File Offset: 0x000B59CB
			public virtual string InstantMessagingCertificateThumbprint
			{
				set
				{
					base.PowerSharpParameters["InstantMessagingCertificateThumbprint"] = value;
				}
			}

			// Token: 0x170053A9 RID: 21417
			// (set) Token: 0x06007B13 RID: 31507 RVA: 0x000B77DE File Offset: 0x000B59DE
			public virtual string InstantMessagingServerName
			{
				set
				{
					base.PowerSharpParameters["InstantMessagingServerName"] = value;
				}
			}

			// Token: 0x170053AA RID: 21418
			// (set) Token: 0x06007B14 RID: 31508 RVA: 0x000B77F1 File Offset: 0x000B59F1
			public virtual bool? RedirectToOptimalOWAServer
			{
				set
				{
					base.PowerSharpParameters["RedirectToOptimalOWAServer"] = value;
				}
			}

			// Token: 0x170053AB RID: 21419
			// (set) Token: 0x06007B15 RID: 31509 RVA: 0x000B7809 File Offset: 0x000B5A09
			public virtual int? DefaultClientLanguage
			{
				set
				{
					base.PowerSharpParameters["DefaultClientLanguage"] = value;
				}
			}

			// Token: 0x170053AC RID: 21420
			// (set) Token: 0x06007B16 RID: 31510 RVA: 0x000B7821 File Offset: 0x000B5A21
			public virtual int LogonAndErrorLanguage
			{
				set
				{
					base.PowerSharpParameters["LogonAndErrorLanguage"] = value;
				}
			}

			// Token: 0x170053AD RID: 21421
			// (set) Token: 0x06007B17 RID: 31511 RVA: 0x000B7839 File Offset: 0x000B5A39
			public virtual bool? UseGB18030
			{
				set
				{
					base.PowerSharpParameters["UseGB18030"] = value;
				}
			}

			// Token: 0x170053AE RID: 21422
			// (set) Token: 0x06007B18 RID: 31512 RVA: 0x000B7851 File Offset: 0x000B5A51
			public virtual bool? UseISO885915
			{
				set
				{
					base.PowerSharpParameters["UseISO885915"] = value;
				}
			}

			// Token: 0x170053AF RID: 21423
			// (set) Token: 0x06007B19 RID: 31513 RVA: 0x000B7869 File Offset: 0x000B5A69
			public virtual OutboundCharsetOptions? OutboundCharset
			{
				set
				{
					base.PowerSharpParameters["OutboundCharset"] = value;
				}
			}

			// Token: 0x170053B0 RID: 21424
			// (set) Token: 0x06007B1A RID: 31514 RVA: 0x000B7881 File Offset: 0x000B5A81
			public virtual bool? GlobalAddressListEnabled
			{
				set
				{
					base.PowerSharpParameters["GlobalAddressListEnabled"] = value;
				}
			}

			// Token: 0x170053B1 RID: 21425
			// (set) Token: 0x06007B1B RID: 31515 RVA: 0x000B7899 File Offset: 0x000B5A99
			public virtual bool? OrganizationEnabled
			{
				set
				{
					base.PowerSharpParameters["OrganizationEnabled"] = value;
				}
			}

			// Token: 0x170053B2 RID: 21426
			// (set) Token: 0x06007B1C RID: 31516 RVA: 0x000B78B1 File Offset: 0x000B5AB1
			public virtual bool? ExplicitLogonEnabled
			{
				set
				{
					base.PowerSharpParameters["ExplicitLogonEnabled"] = value;
				}
			}

			// Token: 0x170053B3 RID: 21427
			// (set) Token: 0x06007B1D RID: 31517 RVA: 0x000B78C9 File Offset: 0x000B5AC9
			public virtual bool? OWALightEnabled
			{
				set
				{
					base.PowerSharpParameters["OWALightEnabled"] = value;
				}
			}

			// Token: 0x170053B4 RID: 21428
			// (set) Token: 0x06007B1E RID: 31518 RVA: 0x000B78E1 File Offset: 0x000B5AE1
			public virtual bool? DelegateAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["DelegateAccessEnabled"] = value;
				}
			}

			// Token: 0x170053B5 RID: 21429
			// (set) Token: 0x06007B1F RID: 31519 RVA: 0x000B78F9 File Offset: 0x000B5AF9
			public virtual bool? IRMEnabled
			{
				set
				{
					base.PowerSharpParameters["IRMEnabled"] = value;
				}
			}

			// Token: 0x170053B6 RID: 21430
			// (set) Token: 0x06007B20 RID: 31520 RVA: 0x000B7911 File Offset: 0x000B5B11
			public virtual bool? CalendarEnabled
			{
				set
				{
					base.PowerSharpParameters["CalendarEnabled"] = value;
				}
			}

			// Token: 0x170053B7 RID: 21431
			// (set) Token: 0x06007B21 RID: 31521 RVA: 0x000B7929 File Offset: 0x000B5B29
			public virtual bool? ContactsEnabled
			{
				set
				{
					base.PowerSharpParameters["ContactsEnabled"] = value;
				}
			}

			// Token: 0x170053B8 RID: 21432
			// (set) Token: 0x06007B22 RID: 31522 RVA: 0x000B7941 File Offset: 0x000B5B41
			public virtual bool? TasksEnabled
			{
				set
				{
					base.PowerSharpParameters["TasksEnabled"] = value;
				}
			}

			// Token: 0x170053B9 RID: 21433
			// (set) Token: 0x06007B23 RID: 31523 RVA: 0x000B7959 File Offset: 0x000B5B59
			public virtual bool? JournalEnabled
			{
				set
				{
					base.PowerSharpParameters["JournalEnabled"] = value;
				}
			}

			// Token: 0x170053BA RID: 21434
			// (set) Token: 0x06007B24 RID: 31524 RVA: 0x000B7971 File Offset: 0x000B5B71
			public virtual bool? NotesEnabled
			{
				set
				{
					base.PowerSharpParameters["NotesEnabled"] = value;
				}
			}

			// Token: 0x170053BB RID: 21435
			// (set) Token: 0x06007B25 RID: 31525 RVA: 0x000B7989 File Offset: 0x000B5B89
			public virtual bool? RemindersAndNotificationsEnabled
			{
				set
				{
					base.PowerSharpParameters["RemindersAndNotificationsEnabled"] = value;
				}
			}

			// Token: 0x170053BC RID: 21436
			// (set) Token: 0x06007B26 RID: 31526 RVA: 0x000B79A1 File Offset: 0x000B5BA1
			public virtual bool? PremiumClientEnabled
			{
				set
				{
					base.PowerSharpParameters["PremiumClientEnabled"] = value;
				}
			}

			// Token: 0x170053BD RID: 21437
			// (set) Token: 0x06007B27 RID: 31527 RVA: 0x000B79B9 File Offset: 0x000B5BB9
			public virtual bool? SpellCheckerEnabled
			{
				set
				{
					base.PowerSharpParameters["SpellCheckerEnabled"] = value;
				}
			}

			// Token: 0x170053BE RID: 21438
			// (set) Token: 0x06007B28 RID: 31528 RVA: 0x000B79D1 File Offset: 0x000B5BD1
			public virtual bool? SearchFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["SearchFoldersEnabled"] = value;
				}
			}

			// Token: 0x170053BF RID: 21439
			// (set) Token: 0x06007B29 RID: 31529 RVA: 0x000B79E9 File Offset: 0x000B5BE9
			public virtual bool? SignaturesEnabled
			{
				set
				{
					base.PowerSharpParameters["SignaturesEnabled"] = value;
				}
			}

			// Token: 0x170053C0 RID: 21440
			// (set) Token: 0x06007B2A RID: 31530 RVA: 0x000B7A01 File Offset: 0x000B5C01
			public virtual bool? ThemeSelectionEnabled
			{
				set
				{
					base.PowerSharpParameters["ThemeSelectionEnabled"] = value;
				}
			}

			// Token: 0x170053C1 RID: 21441
			// (set) Token: 0x06007B2B RID: 31531 RVA: 0x000B7A19 File Offset: 0x000B5C19
			public virtual bool? JunkEmailEnabled
			{
				set
				{
					base.PowerSharpParameters["JunkEmailEnabled"] = value;
				}
			}

			// Token: 0x170053C2 RID: 21442
			// (set) Token: 0x06007B2C RID: 31532 RVA: 0x000B7A31 File Offset: 0x000B5C31
			public virtual bool? UMIntegrationEnabled
			{
				set
				{
					base.PowerSharpParameters["UMIntegrationEnabled"] = value;
				}
			}

			// Token: 0x170053C3 RID: 21443
			// (set) Token: 0x06007B2D RID: 31533 RVA: 0x000B7A49 File Offset: 0x000B5C49
			public virtual bool? WSSAccessOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WSSAccessOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x170053C4 RID: 21444
			// (set) Token: 0x06007B2E RID: 31534 RVA: 0x000B7A61 File Offset: 0x000B5C61
			public virtual bool? WSSAccessOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["WSSAccessOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x170053C5 RID: 21445
			// (set) Token: 0x06007B2F RID: 31535 RVA: 0x000B7A79 File Offset: 0x000B5C79
			public virtual bool? ChangePasswordEnabled
			{
				set
				{
					base.PowerSharpParameters["ChangePasswordEnabled"] = value;
				}
			}

			// Token: 0x170053C6 RID: 21446
			// (set) Token: 0x06007B30 RID: 31536 RVA: 0x000B7A91 File Offset: 0x000B5C91
			public virtual bool? UNCAccessOnPublicComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["UNCAccessOnPublicComputersEnabled"] = value;
				}
			}

			// Token: 0x170053C7 RID: 21447
			// (set) Token: 0x06007B31 RID: 31537 RVA: 0x000B7AA9 File Offset: 0x000B5CA9
			public virtual bool? UNCAccessOnPrivateComputersEnabled
			{
				set
				{
					base.PowerSharpParameters["UNCAccessOnPrivateComputersEnabled"] = value;
				}
			}

			// Token: 0x170053C8 RID: 21448
			// (set) Token: 0x06007B32 RID: 31538 RVA: 0x000B7AC1 File Offset: 0x000B5CC1
			public virtual bool? ActiveSyncIntegrationEnabled
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncIntegrationEnabled"] = value;
				}
			}

			// Token: 0x170053C9 RID: 21449
			// (set) Token: 0x06007B33 RID: 31539 RVA: 0x000B7AD9 File Offset: 0x000B5CD9
			public virtual bool? AllAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["AllAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170053CA RID: 21450
			// (set) Token: 0x06007B34 RID: 31540 RVA: 0x000B7AF1 File Offset: 0x000B5CF1
			public virtual bool? RulesEnabled
			{
				set
				{
					base.PowerSharpParameters["RulesEnabled"] = value;
				}
			}

			// Token: 0x170053CB RID: 21451
			// (set) Token: 0x06007B35 RID: 31541 RVA: 0x000B7B09 File Offset: 0x000B5D09
			public virtual bool? PublicFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["PublicFoldersEnabled"] = value;
				}
			}

			// Token: 0x170053CC RID: 21452
			// (set) Token: 0x06007B36 RID: 31542 RVA: 0x000B7B21 File Offset: 0x000B5D21
			public virtual bool? SMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["SMimeEnabled"] = value;
				}
			}

			// Token: 0x170053CD RID: 21453
			// (set) Token: 0x06007B37 RID: 31543 RVA: 0x000B7B39 File Offset: 0x000B5D39
			public virtual bool? RecoverDeletedItemsEnabled
			{
				set
				{
					base.PowerSharpParameters["RecoverDeletedItemsEnabled"] = value;
				}
			}

			// Token: 0x170053CE RID: 21454
			// (set) Token: 0x06007B38 RID: 31544 RVA: 0x000B7B51 File Offset: 0x000B5D51
			public virtual bool? InstantMessagingEnabled
			{
				set
				{
					base.PowerSharpParameters["InstantMessagingEnabled"] = value;
				}
			}

			// Token: 0x170053CF RID: 21455
			// (set) Token: 0x06007B39 RID: 31545 RVA: 0x000B7B69 File Offset: 0x000B5D69
			public virtual bool? TextMessagingEnabled
			{
				set
				{
					base.PowerSharpParameters["TextMessagingEnabled"] = value;
				}
			}

			// Token: 0x170053D0 RID: 21456
			// (set) Token: 0x06007B3A RID: 31546 RVA: 0x000B7B81 File Offset: 0x000B5D81
			public virtual bool? ForceSaveAttachmentFilteringEnabled
			{
				set
				{
					base.PowerSharpParameters["ForceSaveAttachmentFilteringEnabled"] = value;
				}
			}

			// Token: 0x170053D1 RID: 21457
			// (set) Token: 0x06007B3B RID: 31547 RVA: 0x000B7B99 File Offset: 0x000B5D99
			public virtual bool? SilverlightEnabled
			{
				set
				{
					base.PowerSharpParameters["SilverlightEnabled"] = value;
				}
			}

			// Token: 0x170053D2 RID: 21458
			// (set) Token: 0x06007B3C RID: 31548 RVA: 0x000B7BB1 File Offset: 0x000B5DB1
			public virtual bool? PlacesEnabled
			{
				set
				{
					base.PowerSharpParameters["PlacesEnabled"] = value;
				}
			}

			// Token: 0x170053D3 RID: 21459
			// (set) Token: 0x06007B3D RID: 31549 RVA: 0x000B7BC9 File Offset: 0x000B5DC9
			public virtual bool? WeatherEnabled
			{
				set
				{
					base.PowerSharpParameters["WeatherEnabled"] = value;
				}
			}

			// Token: 0x170053D4 RID: 21460
			// (set) Token: 0x06007B3E RID: 31550 RVA: 0x000B7BE1 File Offset: 0x000B5DE1
			public virtual bool? AllowCopyContactsToDeviceAddressBook
			{
				set
				{
					base.PowerSharpParameters["AllowCopyContactsToDeviceAddressBook"] = value;
				}
			}

			// Token: 0x170053D5 RID: 21461
			// (set) Token: 0x06007B3F RID: 31551 RVA: 0x000B7BF9 File Offset: 0x000B5DF9
			public virtual bool? AnonymousFeaturesEnabled
			{
				set
				{
					base.PowerSharpParameters["AnonymousFeaturesEnabled"] = value;
				}
			}

			// Token: 0x170053D6 RID: 21462
			// (set) Token: 0x06007B40 RID: 31552 RVA: 0x000B7C11 File Offset: 0x000B5E11
			public virtual bool? IntegratedFeaturesEnabled
			{
				set
				{
					base.PowerSharpParameters["IntegratedFeaturesEnabled"] = value;
				}
			}

			// Token: 0x170053D7 RID: 21463
			// (set) Token: 0x06007B41 RID: 31553 RVA: 0x000B7C29 File Offset: 0x000B5E29
			public virtual bool? DisplayPhotosEnabled
			{
				set
				{
					base.PowerSharpParameters["DisplayPhotosEnabled"] = value;
				}
			}

			// Token: 0x170053D8 RID: 21464
			// (set) Token: 0x06007B42 RID: 31554 RVA: 0x000B7C41 File Offset: 0x000B5E41
			public virtual bool? SetPhotoEnabled
			{
				set
				{
					base.PowerSharpParameters["SetPhotoEnabled"] = value;
				}
			}

			// Token: 0x170053D9 RID: 21465
			// (set) Token: 0x06007B43 RID: 31555 RVA: 0x000B7C59 File Offset: 0x000B5E59
			public virtual bool? PredictedActionsEnabled
			{
				set
				{
					base.PowerSharpParameters["PredictedActionsEnabled"] = value;
				}
			}

			// Token: 0x170053DA RID: 21466
			// (set) Token: 0x06007B44 RID: 31556 RVA: 0x000B7C71 File Offset: 0x000B5E71
			public virtual bool? UserDiagnosticEnabled
			{
				set
				{
					base.PowerSharpParameters["UserDiagnosticEnabled"] = value;
				}
			}

			// Token: 0x170053DB RID: 21467
			// (set) Token: 0x06007B45 RID: 31557 RVA: 0x000B7C89 File Offset: 0x000B5E89
			public virtual bool? ReportJunkEmailEnabled
			{
				set
				{
					base.PowerSharpParameters["ReportJunkEmailEnabled"] = value;
				}
			}

			// Token: 0x170053DC RID: 21468
			// (set) Token: 0x06007B46 RID: 31558 RVA: 0x000B7CA1 File Offset: 0x000B5EA1
			public virtual WebPartsFrameOptions? WebPartsFrameOptionsType
			{
				set
				{
					base.PowerSharpParameters["WebPartsFrameOptionsType"] = value;
				}
			}

			// Token: 0x170053DD RID: 21469
			// (set) Token: 0x06007B47 RID: 31559 RVA: 0x000B7CB9 File Offset: 0x000B5EB9
			public virtual AllowOfflineOnEnum AllowOfflineOn
			{
				set
				{
					base.PowerSharpParameters["AllowOfflineOn"] = value;
				}
			}

			// Token: 0x170053DE RID: 21470
			// (set) Token: 0x06007B48 RID: 31560 RVA: 0x000B7CD1 File Offset: 0x000B5ED1
			public virtual string SetPhotoURL
			{
				set
				{
					base.PowerSharpParameters["SetPhotoURL"] = value;
				}
			}

			// Token: 0x170053DF RID: 21471
			// (set) Token: 0x06007B49 RID: 31561 RVA: 0x000B7CE4 File Offset: 0x000B5EE4
			public virtual InstantMessagingTypeOptions? InstantMessagingType
			{
				set
				{
					base.PowerSharpParameters["InstantMessagingType"] = value;
				}
			}

			// Token: 0x170053E0 RID: 21472
			// (set) Token: 0x06007B4A RID: 31562 RVA: 0x000B7CFC File Offset: 0x000B5EFC
			public virtual Uri Exchange2003Url
			{
				set
				{
					base.PowerSharpParameters["Exchange2003Url"] = value;
				}
			}

			// Token: 0x170053E1 RID: 21473
			// (set) Token: 0x06007B4B RID: 31563 RVA: 0x000B7D0F File Offset: 0x000B5F0F
			public virtual Uri FailbackUrl
			{
				set
				{
					base.PowerSharpParameters["FailbackUrl"] = value;
				}
			}

			// Token: 0x170053E2 RID: 21474
			// (set) Token: 0x06007B4C RID: 31564 RVA: 0x000B7D22 File Offset: 0x000B5F22
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170053E3 RID: 21475
			// (set) Token: 0x06007B4D RID: 31565 RVA: 0x000B7D3A File Offset: 0x000B5F3A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170053E4 RID: 21476
			// (set) Token: 0x06007B4E RID: 31566 RVA: 0x000B7D52 File Offset: 0x000B5F52
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170053E5 RID: 21477
			// (set) Token: 0x06007B4F RID: 31567 RVA: 0x000B7D6A File Offset: 0x000B5F6A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170053E6 RID: 21478
			// (set) Token: 0x06007B50 RID: 31568 RVA: 0x000B7D82 File Offset: 0x000B5F82
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
