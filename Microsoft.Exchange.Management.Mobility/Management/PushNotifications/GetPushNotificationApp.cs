using System;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.Publishers;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x0200004B RID: 75
	[Cmdlet("Get", "PushNotificationApp", DefaultParameterSetName = "Identity")]
	[OutputType(new Type[]
	{
		typeof(PushNotificationAppPresentationObject)
	})]
	public sealed class GetPushNotificationApp : GetSystemConfigurationObjectTask<PushNotificationAppIdParameter, PushNotificationApp>
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000C574 File Offset: 0x0000A774
		protected override ObjectId RootId
		{
			get
			{
				IConfigurationSession configurationSession = base.DataSession as IConfigurationSession;
				return configurationSession.GetOrgContainerId().GetDescendantId(PushNotificationApp.RdnContainer);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000C59D File Offset: 0x0000A79D
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (base.Fields["Platform"] != null)
				{
					return base.OptionalIdentityData.AdditionalFilter;
				}
				return base.InternalFilter;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000C5C6 File Offset: 0x0000A7C6
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000C5F1 File Offset: 0x0000A7F1
		[Parameter]
		public PushNotificationPlatform Platform
		{
			get
			{
				if (base.Fields["Platform"] != null)
				{
					return (PushNotificationPlatform)base.Fields["Platform"];
				}
				return PushNotificationPlatform.None;
			}
			set
			{
				base.Fields["Platform"] = value;
				base.OptionalIdentityData.AdditionalFilter = new ComparisonFilter(ComparisonOperator.Equal, PushNotificationAppSchema.Platform, base.Fields["Platform"]);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000C62F File Offset: 0x0000A82F
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000C65A File Offset: 0x0000A85A
		[Parameter]
		public bool Enabled
		{
			get
			{
				return base.Fields["Enabled"] == null || (bool)base.Fields["Enabled"];
			}
			set
			{
				base.Fields["Enabled"] = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000C672 File Offset: 0x0000A872
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000C698 File Offset: 0x0000A898
		[Parameter]
		public SwitchParameter UseClearTextAuthenticationKeys
		{
			get
			{
				return (SwitchParameter)(base.Fields["UseClearTextAuthenticationKeys"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["UseClearTextAuthenticationKeys"] = value;
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
		protected override void WriteResult(IConfigurable dataObject)
		{
			PushNotificationApp pushNotificationApp = dataObject as PushNotificationApp;
			PushNotificationAppPresentationObject pushNotificationAppPresentationObject = new PushNotificationAppPresentationObject(pushNotificationApp);
			if (!base.Fields.IsModified("Enabled") || pushNotificationAppPresentationObject.Enabled == this.Enabled)
			{
				if (this.UseClearTextAuthenticationKeys && pushNotificationApp.IsAuthenticationKeyEncrypted != null && pushNotificationApp.IsAuthenticationKeyEncrypted.Value)
				{
					PushNotificationDataProtector pushNotificationDataProtector = new PushNotificationDataProtector(null);
					pushNotificationApp.AuthenticationKey = pushNotificationDataProtector.Decrypt(pushNotificationApp.AuthenticationKey).AsUnsecureString();
					pushNotificationApp.IsAuthenticationKeyEncrypted = new bool?(false);
					pushNotificationAppPresentationObject = new PushNotificationAppPresentationObject(pushNotificationApp);
				}
				base.WriteResult(pushNotificationAppPresentationObject);
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000C76A File Offset: 0x0000A96A
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 156, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\PushNotifications\\GetPushNotificationApp.cs");
		}

		// Token: 0x040000BF RID: 191
		private const string EnabledParameterName = "Enabled";
	}
}
