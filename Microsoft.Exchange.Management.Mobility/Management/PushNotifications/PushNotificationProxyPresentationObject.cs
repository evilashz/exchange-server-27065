using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x02000053 RID: 83
	[Serializable]
	public sealed class PushNotificationProxyPresentationObject : ADPresentationObject
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0000E04C File Offset: 0x0000C24C
		internal PushNotificationProxyPresentationObject(PushNotificationApp pushNotificationApp) : base(pushNotificationApp)
		{
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000E055 File Offset: 0x0000C255
		public PushNotificationProxyPresentationObject()
		{
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000E05D File Offset: 0x0000C25D
		public string DisplayName
		{
			get
			{
				return (string)this[PushNotificationProxyPresentationSchema.DisplayName];
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000E06F File Offset: 0x0000C26F
		public bool? Enabled
		{
			get
			{
				return new bool?((bool)(this[PushNotificationProxyPresentationSchema.Enabled] ?? PushNotificationProxyPresentationObject.ProxyDefaults.Enabled));
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000E099 File Offset: 0x0000C299
		public string Organization
		{
			get
			{
				return (string)(this[PushNotificationProxyPresentationSchema.Organization] ?? PushNotificationProxyPresentationObject.ProxyDefaults.AuthenticationKey);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000E0B9 File Offset: 0x0000C2B9
		public string Uri
		{
			get
			{
				return (string)(this[PushNotificationProxyPresentationSchema.Uri] ?? PushNotificationProxyPresentationObject.ProxyDefaults.Url);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000E0D9 File Offset: 0x0000C2D9
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return PushNotificationProxyPresentationObject.SchemaInstance;
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		private static PushNotificationApp BuildProxyDefaults()
		{
			return new PushNotificationApp
			{
				Enabled = new bool?(false)
			};
		}

		// Token: 0x040000F8 RID: 248
		private static readonly PushNotificationProxyPresentationSchema SchemaInstance = ObjectSchema.GetInstance<PushNotificationProxyPresentationSchema>();

		// Token: 0x040000F9 RID: 249
		private static readonly PushNotificationApp ProxyDefaults = PushNotificationProxyPresentationObject.BuildProxyDefaults();
	}
}
