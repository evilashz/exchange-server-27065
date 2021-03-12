using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000532 RID: 1330
	[Serializable]
	public class OutlookProvider : ADConfigurationObject
	{
		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x000E1FBC File Offset: 0x000E01BC
		internal override ADObjectSchema Schema
		{
			get
			{
				return OutlookProvider.schema;
			}
		}

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06003B28 RID: 15144 RVA: 0x000E1FC3 File Offset: 0x000E01C3
		internal override string MostDerivedObjectClass
		{
			get
			{
				return OutlookProvider.mostDerivedClass;
			}
		}

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06003B29 RID: 15145 RVA: 0x000E1FCA File Offset: 0x000E01CA
		// (set) Token: 0x06003B2A RID: 15146 RVA: 0x000E1FDC File Offset: 0x000E01DC
		[Parameter(Mandatory = false)]
		public string CertPrincipalName
		{
			get
			{
				return (string)this[OutlookProviderSchema.CertPrincipalName];
			}
			set
			{
				this[OutlookProviderSchema.CertPrincipalName] = value;
			}
		}

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x06003B2B RID: 15147 RVA: 0x000E1FEA File Offset: 0x000E01EA
		// (set) Token: 0x06003B2C RID: 15148 RVA: 0x000E1FFC File Offset: 0x000E01FC
		[Parameter(Mandatory = false)]
		public string Server
		{
			get
			{
				return (string)this[OutlookProviderSchema.Server];
			}
			set
			{
				this[OutlookProviderSchema.Server] = value;
			}
		}

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x06003B2D RID: 15149 RVA: 0x000E200A File Offset: 0x000E020A
		// (set) Token: 0x06003B2E RID: 15150 RVA: 0x000E2026 File Offset: 0x000E0226
		[Parameter(Mandatory = false)]
		public int TTL
		{
			get
			{
				return (int)(this[OutlookProviderSchema.TTL] ?? 1);
			}
			set
			{
				this[OutlookProviderSchema.TTL] = value;
			}
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06003B2F RID: 15151 RVA: 0x000E2039 File Offset: 0x000E0239
		// (set) Token: 0x06003B30 RID: 15152 RVA: 0x000E2055 File Offset: 0x000E0255
		[Parameter(Mandatory = false)]
		public OutlookProviderFlags OutlookProviderFlags
		{
			get
			{
				return (OutlookProviderFlags)(this[OutlookProviderSchema.Flags] ?? OutlookProviderFlags.None);
			}
			set
			{
				this[OutlookProviderSchema.Flags] = value;
			}
		}

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06003B31 RID: 15153 RVA: 0x000E2068 File Offset: 0x000E0268
		// (set) Token: 0x06003B32 RID: 15154 RVA: 0x000E20DC File Offset: 0x000E02DC
		[Parameter(Mandatory = false)]
		public string[] RequiredClientVersions
		{
			get
			{
				ClientVersionCollection clientVersionCollection = (ClientVersionCollection)this[OutlookProviderSchema.RequiredClientVersions];
				if (clientVersionCollection != null)
				{
					string[] array = new string[clientVersionCollection.Count];
					int num = 0;
					foreach (ClientVersion clientVersion in clientVersionCollection)
					{
						array[num] = clientVersion.ToString();
					}
					return array;
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					ClientVersionCollection clientVersionCollection = new ClientVersionCollection();
					for (int i = 0; i < value.Length; i++)
					{
						string clientVersionString = value[i];
						ClientVersion item = ClientVersion.Parse(clientVersionString);
						clientVersionCollection.Add(item);
					}
					this[OutlookProviderSchema.RequiredClientVersions] = clientVersionCollection;
					return;
				}
				this[OutlookProviderSchema.RequiredClientVersions] = null;
			}
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x000E2132 File Offset: 0x000E0332
		public ClientVersionCollection GetRequiredClientVersionCollection()
		{
			return (ClientVersionCollection)this[OutlookProviderSchema.RequiredClientVersions];
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x000E2144 File Offset: 0x000E0344
		internal static ADObjectId GetParentContainer(ITopologyConfigurationSession adSession)
		{
			ADObjectId clientAccessContainerId = adSession.GetClientAccessContainerId();
			ADObjectId relativePath = new ADObjectId("CN=AutoDiscover");
			ADObjectId relativePath2 = new ADObjectId("CN=Outlook");
			return clientAccessContainerId.GetDescendantId(relativePath).GetDescendantId(relativePath2);
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x000E217D File Offset: 0x000E037D
		internal void InitializeDefaults()
		{
			this.TTL = 1;
		}

		// Token: 0x0400281C RID: 10268
		private static OutlookProviderSchema schema = ObjectSchema.GetInstance<OutlookProviderSchema>();

		// Token: 0x0400281D RID: 10269
		private static string mostDerivedClass = "msExchAutoDiscoverConfig";
	}
}
