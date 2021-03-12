using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000432 RID: 1074
	[Serializable]
	public class EdgeSyncEhfConnector : EdgeSyncConnector
	{
		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x000C260C File Offset: 0x000C080C
		// (set) Token: 0x06003045 RID: 12357 RVA: 0x000C2623 File Offset: 0x000C0823
		[Parameter(Mandatory = false)]
		public Uri ProvisioningUrl
		{
			get
			{
				return (Uri)this.propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.ProvisioningUrl];
			}
			set
			{
				this.propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.ProvisioningUrl] = value;
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06003046 RID: 12358 RVA: 0x000C2636 File Offset: 0x000C0836
		// (set) Token: 0x06003047 RID: 12359 RVA: 0x000C264D File Offset: 0x000C084D
		[Parameter(Mandatory = false)]
		public string PrimaryLeaseLocation
		{
			get
			{
				return (string)this.propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.PrimaryLeaseLocation];
			}
			set
			{
				this.propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.PrimaryLeaseLocation] = value;
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06003048 RID: 12360 RVA: 0x000C2660 File Offset: 0x000C0860
		// (set) Token: 0x06003049 RID: 12361 RVA: 0x000C2677 File Offset: 0x000C0877
		[Parameter(Mandatory = false)]
		public string BackupLeaseLocation
		{
			get
			{
				return (string)this.propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.BackupLeaseLocation];
			}
			set
			{
				this.propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.BackupLeaseLocation] = value;
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x000C268A File Offset: 0x000C088A
		// (set) Token: 0x0600304B RID: 12363 RVA: 0x000C26A1 File Offset: 0x000C08A1
		[Parameter(Mandatory = false)]
		public PSCredential AuthenticationCredential
		{
			get
			{
				return (PSCredential)this.propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.AuthenticationCredential];
			}
			set
			{
				this.propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.AuthenticationCredential] = value;
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x0600304C RID: 12364 RVA: 0x000C26B4 File Offset: 0x000C08B4
		// (set) Token: 0x0600304D RID: 12365 RVA: 0x000C26CB File Offset: 0x000C08CB
		[Parameter(Mandatory = false)]
		public string ResellerId
		{
			get
			{
				return (string)this.propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.ResellerId];
			}
			set
			{
				this.propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.ResellerId] = value;
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x0600304E RID: 12366 RVA: 0x000C26DE File Offset: 0x000C08DE
		// (set) Token: 0x0600304F RID: 12367 RVA: 0x000C26F0 File Offset: 0x000C08F0
		[Parameter(Mandatory = false)]
		public bool AdminSyncEnabled
		{
			get
			{
				return (bool)this[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.AdminSyncEnabled];
			}
			set
			{
				this[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.AdminSyncEnabled] = value;
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x000C2703 File Offset: 0x000C0903
		// (set) Token: 0x06003051 RID: 12369 RVA: 0x000C2715 File Offset: 0x000C0915
		[Parameter(Mandatory = false)]
		public int Version
		{
			get
			{
				return (int)this[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.Version];
			}
			set
			{
				this[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.Version] = value;
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06003052 RID: 12370 RVA: 0x000C2728 File Offset: 0x000C0928
		internal override ADObjectSchema Schema
		{
			get
			{
				return EdgeSyncEhfConnector.schema;
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06003053 RID: 12371 RVA: 0x000C272F File Offset: 0x000C092F
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchEdgeSyncEhfConnector";
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x000C2736 File Offset: 0x000C0936
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000C274C File Offset: 0x000C094C
		internal static object AuthenticationCredentialGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.UserName];
			string text2 = (string)propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.Password];
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				SecureString password = text2.ConvertToSecureString();
				return new PSCredential(text, password);
			}
			return null;
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000C279C File Offset: 0x000C099C
		internal static void AuthenticationCredentialSetter(object value, IPropertyBag propertyBag)
		{
			PSCredential pscredential = value as PSCredential;
			if (pscredential == null)
			{
				propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.UserName] = null;
				propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.Password] = null;
				return;
			}
			string empty = string.Empty;
			if (pscredential.Password == null || pscredential.Password.Length == 0)
			{
				return;
			}
			string value2 = pscredential.Password.ConvertToUnsecureString();
			propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.UserName] = pscredential.UserName;
			propertyBag[EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.Password] = value2;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000C2811 File Offset: 0x000C0A11
		internal void SetIdentity(ObjectId id)
		{
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = id;
		}

		// Token: 0x0400207A RID: 8314
		internal const string MostDerivedClass = "msExchEdgeSyncEhfConnector";

		// Token: 0x0400207B RID: 8315
		private static EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema schema = ObjectSchema.GetInstance<EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema>();

		// Token: 0x02000433 RID: 1075
		[Flags]
		internal enum EdgeSyncEhfConnectorFlags
		{
			// Token: 0x0400207D RID: 8317
			None = 0,
			// Token: 0x0400207E RID: 8318
			AdminSyncEnabled = 1,
			// Token: 0x0400207F RID: 8319
			All = 1
		}

		// Token: 0x02000434 RID: 1076
		internal class EdgeSyncEhfConnectorSchema : EdgeSyncConnectorSchema
		{
			// Token: 0x04002080 RID: 8320
			public static readonly ADPropertyDefinition ProvisioningUrl = new ADPropertyDefinition("ProvisioningUrl", ExchangeObjectVersion.Exchange2007, typeof(Uri), "msExchEdgeSyncEHFProvisioningUrl", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
			{
				new UriKindConstraint(UriKind.Absolute)
			}, new PropertyDefinitionConstraint[]
			{
				new UriKindConstraint(UriKind.Absolute)
			}, null, null);

			// Token: 0x04002081 RID: 8321
			public static readonly ADPropertyDefinition PrimaryLeaseLocation = new ADPropertyDefinition("PrimaryLeaseLocation", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchEdgeSyncEHFPrimaryLeaseLocation", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

			// Token: 0x04002082 RID: 8322
			public static readonly ADPropertyDefinition BackupLeaseLocation = new ADPropertyDefinition("BackupLeaseLocation", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchEdgeSyncEHFBackupLeaseLocation", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

			// Token: 0x04002083 RID: 8323
			public static readonly ADPropertyDefinition UserName = new ADPropertyDefinition("UserName", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchEdgeSyncEHFUserName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

			// Token: 0x04002084 RID: 8324
			public static readonly ADPropertyDefinition Password = new ADPropertyDefinition("Password", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchEdgeSyncEHFPassword", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

			// Token: 0x04002085 RID: 8325
			public static readonly ADPropertyDefinition ResellerId = new ADPropertyDefinition("ResellerId", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchEdgeSyncEHFResellerID", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
			{
				new Int32ParsableStringConstraint()
			}, null, null);

			// Token: 0x04002086 RID: 8326
			public static readonly ADPropertyDefinition AuthenticationCredential = new ADPropertyDefinition("AuthenticationCredential", ExchangeObjectVersion.Exchange2007, typeof(PSCredential), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
			{
				EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.UserName,
				EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.Password
			}, null, new GetterDelegate(EdgeSyncEhfConnector.AuthenticationCredentialGetter), new SetterDelegate(EdgeSyncEhfConnector.AuthenticationCredentialSetter), null, null);

			// Token: 0x04002087 RID: 8327
			public static readonly ADPropertyDefinition Flags = new ADPropertyDefinition("EdgeSyncEhfConnectorFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchEdgeSyncEHFFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

			// Token: 0x04002088 RID: 8328
			public static readonly ADPropertyDefinition AdminSyncEnabled = new ADPropertyDefinition("AdminSyncEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
			{
				EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.Flags
			}, null, ADObject.FlagGetterDelegate(EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.Flags, 1), ADObject.FlagSetterDelegate(EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema.Flags, 1), null, null);

			// Token: 0x04002089 RID: 8329
			public static readonly ADPropertyDefinition Version = new ADPropertyDefinition("Version", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchEdgeSyncConnectorVersion", ADPropertyDefinitionFlags.PersistDefaultValue, 1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
		}
	}
}
