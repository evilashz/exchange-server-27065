using System;
using System.Collections;
using System.Linq;
using System.Management.Automation;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006E9 RID: 1769
	[Serializable]
	public class ClientAccessServer : ADPresentationObject, IDeserializationCallback
	{
		// Token: 0x17001B28 RID: 6952
		// (get) Token: 0x060052B8 RID: 21176 RVA: 0x0012FB51 File Offset: 0x0012DD51
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return ClientAccessServer.schema;
			}
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x0012FB58 File Offset: 0x0012DD58
		public ClientAccessServer()
		{
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x0012FB60 File Offset: 0x0012DD60
		public ClientAccessServer(Server dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001B29 RID: 6953
		// (get) Token: 0x060052BB RID: 21179 RVA: 0x0012FB69 File Offset: 0x0012DD69
		public new string Name
		{
			get
			{
				return (string)this[ADObjectSchema.Name];
			}
		}

		// Token: 0x17001B2A RID: 6954
		// (get) Token: 0x060052BC RID: 21180 RVA: 0x0012FB7B File Offset: 0x0012DD7B
		public string Fqdn
		{
			get
			{
				return (string)this[ServerSchema.Fqdn];
			}
		}

		// Token: 0x17001B2B RID: 6955
		// (get) Token: 0x060052BD RID: 21181 RVA: 0x0012FB8D File Offset: 0x0012DD8D
		// (set) Token: 0x060052BE RID: 21182 RVA: 0x0012FB9F File Offset: 0x0012DD9F
		public ADObjectId ClientAccessArray
		{
			get
			{
				return (ADObjectId)this[ClientAccessServerSchema.ClientAccessArray];
			}
			internal set
			{
				this[ClientAccessServerSchema.ClientAccessArray] = value;
			}
		}

		// Token: 0x17001B2C RID: 6956
		// (get) Token: 0x060052BF RID: 21183 RVA: 0x0012FBAD File Offset: 0x0012DDAD
		// (set) Token: 0x060052C0 RID: 21184 RVA: 0x0012FBB5 File Offset: 0x0012DDB5
		public bool? OutlookAnywhereEnabled
		{
			get
			{
				return this.isRpcHttpEnabled;
			}
			internal set
			{
				this.isRpcHttpEnabled = value;
			}
		}

		// Token: 0x17001B2D RID: 6957
		// (get) Token: 0x060052C1 RID: 21185 RVA: 0x0012FBBE File Offset: 0x0012DDBE
		// (set) Token: 0x060052C2 RID: 21186 RVA: 0x0012FBC6 File Offset: 0x0012DDC6
		public Fqdn AutoDiscoverServiceCN
		{
			get
			{
				return this.scpAutoDiscoverServiceCN;
			}
			internal set
			{
				this.scpAutoDiscoverServiceCN = value;
			}
		}

		// Token: 0x17001B2E RID: 6958
		// (get) Token: 0x060052C3 RID: 21187 RVA: 0x0012FBCF File Offset: 0x0012DDCF
		// (set) Token: 0x060052C4 RID: 21188 RVA: 0x0012FBD7 File Offset: 0x0012DDD7
		public string AutoDiscoverServiceClassName
		{
			get
			{
				return this.scpAutoDiscoverServiceClassName;
			}
			internal set
			{
				this.scpAutoDiscoverServiceClassName = value;
			}
		}

		// Token: 0x17001B2F RID: 6959
		// (get) Token: 0x060052C5 RID: 21189 RVA: 0x0012FBE0 File Offset: 0x0012DDE0
		// (set) Token: 0x060052C6 RID: 21190 RVA: 0x0012FBE8 File Offset: 0x0012DDE8
		public Uri AutoDiscoverServiceInternalUri
		{
			get
			{
				return this.scpAutoDiscoverServiceInternalUri;
			}
			set
			{
				this.scpAutoDiscoverServiceInternalUri = value;
			}
		}

		// Token: 0x17001B30 RID: 6960
		// (get) Token: 0x060052C7 RID: 21191 RVA: 0x0012FBF1 File Offset: 0x0012DDF1
		// (set) Token: 0x060052C8 RID: 21192 RVA: 0x0012FBF9 File Offset: 0x0012DDF9
		public Guid? AutoDiscoverServiceGuid
		{
			get
			{
				return this.scpAutoDiscoverServiceGuid;
			}
			internal set
			{
				this.scpAutoDiscoverServiceGuid = value;
			}
		}

		// Token: 0x17001B31 RID: 6961
		// (get) Token: 0x060052C9 RID: 21193 RVA: 0x0012FC02 File Offset: 0x0012DE02
		// (set) Token: 0x060052CA RID: 21194 RVA: 0x0012FC0A File Offset: 0x0012DE0A
		public MultiValuedProperty<string> AutoDiscoverSiteScope
		{
			get
			{
				return this.scpAutoDiscoverSiteScope;
			}
			set
			{
				this.scpAutoDiscoverSiteScope = value;
			}
		}

		// Token: 0x17001B32 RID: 6962
		// (get) Token: 0x060052CB RID: 21195 RVA: 0x0012FC13 File Offset: 0x0012DE13
		// (set) Token: 0x060052CC RID: 21196 RVA: 0x0012FC1B File Offset: 0x0012DE1B
		public AlternateServiceAccountConfiguration AlternateServiceAccountConfiguration { get; internal set; }

		// Token: 0x17001B33 RID: 6963
		// (get) Token: 0x060052CD RID: 21197 RVA: 0x0012FC24 File Offset: 0x0012DE24
		// (set) Token: 0x060052CE RID: 21198 RVA: 0x0012FC36 File Offset: 0x0012DE36
		[Parameter(Mandatory = false)]
		public bool IsOutOfService
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.IsOutOfService];
			}
			set
			{
				this[ActiveDirectoryServerSchema.IsOutOfService] = value;
			}
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x0012FC4C File Offset: 0x0012DE4C
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			PSObject psobject = sender as PSObject;
			if (psobject != null && this.AlternateServiceAccountConfiguration != null)
			{
				PSObject psobject2 = (PSObject)psobject.Properties["_AlternateServiceAccountConfiguration_AllCredentials_Password"].Value;
				if (psobject2 != null)
				{
					this.AlternateServiceAccountConfiguration.ApplyPasswords(((IEnumerable)psobject2.BaseObject).Cast<SecureString>().ToArray<SecureString>());
				}
			}
		}

		// Token: 0x040037DD RID: 14301
		private static ClientAccessServerSchema schema = ObjectSchema.GetInstance<ClientAccessServerSchema>();

		// Token: 0x040037DE RID: 14302
		private bool? isRpcHttpEnabled;

		// Token: 0x040037DF RID: 14303
		private Fqdn scpAutoDiscoverServiceCN;

		// Token: 0x040037E0 RID: 14304
		private string scpAutoDiscoverServiceClassName;

		// Token: 0x040037E1 RID: 14305
		private Uri scpAutoDiscoverServiceInternalUri;

		// Token: 0x040037E2 RID: 14306
		private Guid? scpAutoDiscoverServiceGuid;

		// Token: 0x040037E3 RID: 14307
		private MultiValuedProperty<string> scpAutoDiscoverSiteScope;
	}
}
