﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000031 RID: 49
	internal static class WellKnownPermission
	{
		// Token: 0x06000130 RID: 304 RVA: 0x000064B8 File Offset: 0x000046B8
		public static Guid ToGuid(Permission index)
		{
			Guid result;
			if (!WellKnownPermission.permissionDefinitions.TryGetValue(index, out result))
			{
				throw new ArgumentOutOfRangeException("Not a specific permission or recognized value.", index, "index");
			}
			return result;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000064EC File Offset: 0x000046EC
		public static IList<Guid> ToGuids(Permission permissions)
		{
			List<Guid> list = new List<Guid>();
			if (permissions != Permission.None)
			{
				Permission permission = Permission.SMTPSubmit;
				int i = 0;
				while (i < 32)
				{
					Guid item;
					if ((permissions & permission) == permission && WellKnownPermission.permissionDefinitions.TryGetValue(permission, out item))
					{
						list.Add(item);
					}
					i++;
					permission <<= 1;
				}
			}
			return list;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006534 File Offset: 0x00004734
		public static Permission ToPermission(Guid guid)
		{
			Permission result;
			if (!WellKnownPermission.guidDefinitions.TryGetValue(guid, out result))
			{
				return Permission.None;
			}
			return result;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006554 File Offset: 0x00004754
		private static Dictionary<Permission, Guid> BuildPermissionDictionary()
		{
			return new Dictionary<Permission, Guid>(20)
			{
				{
					Permission.SMTPSubmit,
					WellKnownPermission.SMTPSubmitGuid
				},
				{
					Permission.SMTPSubmitForMLS,
					WellKnownPermission.SMTPSubmitForMLSGuid
				},
				{
					Permission.SMTPAcceptAnyRecipient,
					WellKnownPermission.SMTPAcceptAnyRecipientGuid
				},
				{
					Permission.SMTPAcceptAuthenticationFlag,
					WellKnownPermission.SMTPAcceptAuthenticationFlagGuid
				},
				{
					Permission.SMTPAcceptAnySender,
					WellKnownPermission.SMTPAcceptAnySenderGuid
				},
				{
					Permission.SMTPAcceptAuthoritativeDomainSender,
					WellKnownPermission.SMTPAcceptAuthoritativeDomainSenderGuid
				},
				{
					Permission.BypassAntiSpam,
					WellKnownPermission.BypassAntiSpamGuid
				},
				{
					Permission.BypassMessageSizeLimit,
					WellKnownPermission.BypassMessageSizeLimitGuid
				},
				{
					Permission.SMTPSendEXCH50,
					WellKnownPermission.SMTPSendEXCH50Guid
				},
				{
					Permission.SMTPAcceptEXCH50,
					WellKnownPermission.SMTPAcceptEXCH50Guid
				},
				{
					Permission.AcceptRoutingHeaders,
					WellKnownPermission.AcceptRoutingHeadersGuid
				},
				{
					Permission.AcceptForestHeaders,
					WellKnownPermission.AcceptForestHeadersGuid
				},
				{
					Permission.AcceptOrganizationHeaders,
					WellKnownPermission.AcceptOrganizationHeadersGuid
				},
				{
					Permission.SendRoutingHeaders,
					WellKnownPermission.SendRoutingHeadersGuid
				},
				{
					Permission.SendForestHeaders,
					WellKnownPermission.SendForestHeadersGuid
				},
				{
					Permission.SendOrganizationHeaders,
					WellKnownPermission.SendOrganizationHeadersGuid
				},
				{
					Permission.SendAs,
					WellKnownPermission.SendAsGuid
				},
				{
					Permission.SMTPSendXShadow,
					WellKnownPermission.SMTPSendXShadowGuid
				},
				{
					Permission.SMTPAcceptXShadow,
					WellKnownPermission.SMTPAcceptXShadowGuid
				},
				{
					Permission.SMTPAcceptXProxyFrom,
					WellKnownPermission.SMTPAcceptXProxyFromGuid
				},
				{
					Permission.SMTPAcceptXSessionParams,
					WellKnownPermission.SMTPAcceptXSessionParamsGuid
				},
				{
					Permission.SMTPAcceptXMessageContextADRecipientCache,
					WellKnownPermission.SMTPAcceptXMessageContextADRecipientCache
				},
				{
					Permission.SMTPAcceptXMessageContextExtendedProperties,
					WellKnownPermission.SMTPAcceptXMessageContextExtendedProperties
				},
				{
					Permission.SMTPAcceptXMessageContextFastIndex,
					WellKnownPermission.SMTPAcceptXMessageContextFastIndex
				},
				{
					Permission.SMTPAcceptXAttr,
					WellKnownPermission.SMTPAcceptXAttrGuid
				},
				{
					Permission.SMTPAcceptXSysProbe,
					WellKnownPermission.SMTPAcceptXSysProbeGuid
				}
			};
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000066F4 File Offset: 0x000048F4
		private static Dictionary<Guid, Permission> BuildGuidDictionary()
		{
			return new Dictionary<Guid, Permission>(20)
			{
				{
					WellKnownPermission.SMTPSubmitGuid,
					Permission.SMTPSubmit
				},
				{
					WellKnownPermission.SMTPSubmitForMLSGuid,
					Permission.SMTPSubmitForMLS
				},
				{
					WellKnownPermission.SMTPAcceptAnyRecipientGuid,
					Permission.SMTPAcceptAnyRecipient
				},
				{
					WellKnownPermission.SMTPAcceptAuthenticationFlagGuid,
					Permission.SMTPAcceptAuthenticationFlag
				},
				{
					WellKnownPermission.SMTPAcceptAnySenderGuid,
					Permission.SMTPAcceptAnySender
				},
				{
					WellKnownPermission.SMTPAcceptAuthoritativeDomainSenderGuid,
					Permission.SMTPAcceptAuthoritativeDomainSender
				},
				{
					WellKnownPermission.BypassAntiSpamGuid,
					Permission.BypassAntiSpam
				},
				{
					WellKnownPermission.BypassMessageSizeLimitGuid,
					Permission.BypassMessageSizeLimit
				},
				{
					WellKnownPermission.SMTPSendEXCH50Guid,
					Permission.SMTPSendEXCH50
				},
				{
					WellKnownPermission.SMTPAcceptEXCH50Guid,
					Permission.SMTPAcceptEXCH50
				},
				{
					WellKnownPermission.AcceptRoutingHeadersGuid,
					Permission.AcceptRoutingHeaders
				},
				{
					WellKnownPermission.AcceptForestHeadersGuid,
					Permission.AcceptForestHeaders
				},
				{
					WellKnownPermission.AcceptOrganizationHeadersGuid,
					Permission.AcceptOrganizationHeaders
				},
				{
					WellKnownPermission.SendRoutingHeadersGuid,
					Permission.SendRoutingHeaders
				},
				{
					WellKnownPermission.SendForestHeadersGuid,
					Permission.SendForestHeaders
				},
				{
					WellKnownPermission.SendOrganizationHeadersGuid,
					Permission.SendOrganizationHeaders
				},
				{
					WellKnownPermission.SendAsGuid,
					Permission.SendAs
				},
				{
					WellKnownPermission.SMTPSendXShadowGuid,
					Permission.SMTPSendXShadow
				},
				{
					WellKnownPermission.SMTPAcceptXShadowGuid,
					Permission.SMTPAcceptXShadow
				},
				{
					WellKnownPermission.SMTPAcceptXProxyFromGuid,
					Permission.SMTPAcceptXProxyFrom
				},
				{
					WellKnownPermission.SMTPAcceptXSessionParamsGuid,
					Permission.SMTPAcceptXSessionParams
				},
				{
					WellKnownPermission.SMTPAcceptXMessageContextADRecipientCache,
					Permission.SMTPAcceptXMessageContextADRecipientCache
				},
				{
					WellKnownPermission.SMTPAcceptXMessageContextExtendedProperties,
					Permission.SMTPAcceptXMessageContextExtendedProperties
				},
				{
					WellKnownPermission.SMTPAcceptXMessageContextFastIndex,
					Permission.SMTPAcceptXMessageContextFastIndex
				},
				{
					WellKnownPermission.SMTPAcceptXAttrGuid,
					Permission.SMTPAcceptXAttr
				},
				{
					WellKnownPermission.SMTPAcceptXSysProbeGuid,
					Permission.SMTPAcceptXSysProbe
				}
			};
		}

		// Token: 0x040000D9 RID: 217
		[Description("ms-Exch-SMTP-Submit")]
		public static readonly Guid SMTPSubmitGuid = new Guid("a18293f1-0685-4540-aa63-e32df421b3a2");

		// Token: 0x040000DA RID: 218
		[Description("ms-Exch-SMTP-Submit-For-MLS")]
		public static readonly Guid SMTPSubmitForMLSGuid = new Guid("8fc01282-006d-42b1-81e3-c0b46bed3754");

		// Token: 0x040000DB RID: 219
		[Description("ms-Exch-SMTP-Accept-Any-Recipient")]
		public static readonly Guid SMTPAcceptAnyRecipientGuid = new Guid("5c82f031-4e4c-4326-88e1-8c4f0cad9de5");

		// Token: 0x040000DC RID: 220
		[Description("ms-Exch-SMTP-Accept-Authentication-Flag")]
		public static readonly Guid SMTPAcceptAuthenticationFlagGuid = new Guid("1c75aca8-b56b-48b3-a021-858a29fa877b");

		// Token: 0x040000DD RID: 221
		[Description("ms-Exch-SMTP-Accept-Any-Sender")]
		public static readonly Guid SMTPAcceptAnySenderGuid = new Guid("b857b50b-94a2-4b53-93f6-41cebd2fced0");

		// Token: 0x040000DE RID: 222
		[Description("ms-Exch-SMTP-Accept-Authoritative-Domain-Sender")]
		public static readonly Guid SMTPAcceptAuthoritativeDomainSenderGuid = new Guid("c22841f4-96cb-498a-ac02-f9a87c74eb14");

		// Token: 0x040000DF RID: 223
		[Description("ms-Exch-Bypass-Anti-Spam")]
		public static readonly Guid BypassAntiSpamGuid = new Guid("d19299b4-86c2-4c9a-8fa7-acb70c63023a");

		// Token: 0x040000E0 RID: 224
		[Description("ms-Exch-Bypass-Message-Size-Limit")]
		public static readonly Guid BypassMessageSizeLimitGuid = new Guid("6760cfc5-70f4-4ae8-bc39-9522d86ac69b");

		// Token: 0x040000E1 RID: 225
		[Description("ms-Exch-SMTP-Send-Exch50")]
		public static readonly Guid SMTPSendEXCH50Guid = new Guid("11716db4-9647-4bce-8922-1f99e526cb41");

		// Token: 0x040000E2 RID: 226
		[Description("ms-Exch-SMTP-Accept-Exch50")]
		public static readonly Guid SMTPAcceptEXCH50Guid = new Guid("e373fb21-d851-4d15-af23-982f09f2400b");

		// Token: 0x040000E3 RID: 227
		[Description("ms-Exch-Accept-Headers-Routing")]
		public static readonly Guid AcceptRoutingHeadersGuid = new Guid("04031f4f-7c36-43ea-9b49-4bd0f5f1e6af");

		// Token: 0x040000E4 RID: 228
		[Description("ms-Exch-Accept-Headers-Forest")]
		public static readonly Guid AcceptForestHeadersGuid = new Guid("a7a9ea66-e08c-4e23-8fe7-68c40e49c6c0");

		// Token: 0x040000E5 RID: 229
		[Description("ms-Exch-Accept-Headers-Organization")]
		public static readonly Guid AcceptOrganizationHeadersGuid = new Guid("c307dccd-6676-4d19-95c8-d1567fab9820");

		// Token: 0x040000E6 RID: 230
		[Description("ms-Exch-Send-Headers-Routing")]
		public static readonly Guid SendRoutingHeadersGuid = new Guid("eb8c07ad-b5ad-49c3-831e-bc439cca4c2a");

		// Token: 0x040000E7 RID: 231
		[Description("ms-Exch-Send-Headers-Forest")]
		public static readonly Guid SendForestHeadersGuid = new Guid("b3f9f977-552c-4ee6-9781-59280a81417b");

		// Token: 0x040000E8 RID: 232
		[Description("ms-Exch-Send-Headers-Organization")]
		public static readonly Guid SendOrganizationHeadersGuid = new Guid("2f7d0e23-f951-4ed0-8e71-39b6a22fa298");

		// Token: 0x040000E9 RID: 233
		[Description("Send-As")]
		public static readonly Guid SendAsGuid = new Guid("ab721a54-1e2f-11d0-9819-00aa0040529b");

		// Token: 0x040000EA RID: 234
		[Description("ms-Exch-SMTP-Send-XShadow")]
		public static readonly Guid SMTPSendXShadowGuid = new Guid("5BC2ACAB-AD7D-4878-B6CD-3122A47C6A1C");

		// Token: 0x040000EB RID: 235
		[Description("ms-Exch-SMTP-Accept-XShadow")]
		public static readonly Guid SMTPAcceptXShadowGuid = new Guid("811D004B-E2ED-4024-8953-0F0FB0612E47");

		// Token: 0x040000EC RID: 236
		[Description("ms-Exch-SMTP-Accept-XProxyFrom")]
		public static readonly Guid SMTPAcceptXProxyFromGuid = new Guid("5bee2b72-50d7-49c7-ba66-39a25daa1e92");

		// Token: 0x040000ED RID: 237
		[Description("ms-Exch-SMTP-Accept-XSessionParams")]
		public static readonly Guid SMTPAcceptXSessionParamsGuid = new Guid("c08e6405-fccb-4c21-9985-2e865bcf15d5");

		// Token: 0x040000EE RID: 238
		[Description("ms-Exch-SMTP-Accept-XMessageContext-ADRecipientCache")]
		public static readonly Guid SMTPAcceptXMessageContextADRecipientCache = new Guid("b938d9df-1f37-41d3-8cdc-f4148f20076d");

		// Token: 0x040000EF RID: 239
		[Description("ms-Exch-SMTP-Accept-XMessageContext-ExtendedProperties")]
		public static readonly Guid SMTPAcceptXMessageContextExtendedProperties = new Guid("e1f584b5-11c8-451b-91e8-045c75973912");

		// Token: 0x040000F0 RID: 240
		[Description("ms-Exch-SMTP-Accept-XMessageContext-FastIndex")]
		public static readonly Guid SMTPAcceptXMessageContextFastIndex = new Guid("5669b1b6-dca0-429b-8fdb-fc5b07c0f73f");

		// Token: 0x040000F1 RID: 241
		[Description("ms-Exch-SMTP-Accept-XAttr")]
		public static readonly Guid SMTPAcceptXAttrGuid = new Guid("c62e96a4-6f25-497b-8d41-70cbd25eaa99");

		// Token: 0x040000F2 RID: 242
		[Description("ms-Exch-SMTP-Accept-XSysProbe")]
		public static readonly Guid SMTPAcceptXSysProbeGuid = new Guid("5db60ab1-1ad4-4863-b9be-fbc5b00691a3");

		// Token: 0x040000F3 RID: 243
		private static Dictionary<Permission, Guid> permissionDefinitions = WellKnownPermission.BuildPermissionDictionary();

		// Token: 0x040000F4 RID: 244
		private static Dictionary<Guid, Permission> guidDefinitions = WellKnownPermission.BuildGuidDictionary();
	}
}
