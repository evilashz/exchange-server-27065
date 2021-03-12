using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200061D RID: 1565
	[Serializable]
	public sealed class Imap4AdConfiguration : PopImapAdConfiguration
	{
		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x06004A30 RID: 18992 RVA: 0x001126A8 File Offset: 0x001108A8
		internal override ADObjectSchema Schema
		{
			get
			{
				return Imap4AdConfiguration.schema;
			}
		}

		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x06004A31 RID: 18993 RVA: 0x001126AF File Offset: 0x001108AF
		internal override string MostDerivedObjectClass
		{
			get
			{
				return Imap4AdConfiguration.MostDerivedClass;
			}
		}

		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x06004A32 RID: 18994 RVA: 0x001126B6 File Offset: 0x001108B6
		public override string ProtocolName
		{
			get
			{
				return "IMAP4";
			}
		}

		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x06004A33 RID: 18995 RVA: 0x001126BD File Offset: 0x001108BD
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x06004A34 RID: 18996 RVA: 0x001126C5 File Offset: 0x001108C5
		// (set) Token: 0x06004A35 RID: 18997 RVA: 0x001126D7 File Offset: 0x001108D7
		[Parameter(Mandatory = false)]
		public override int MaxCommandSize
		{
			get
			{
				return (int)this[Imap4AdConfigurationSchema.MaxCommandSize];
			}
			set
			{
				this[Imap4AdConfigurationSchema.MaxCommandSize] = value;
			}
		}

		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x06004A36 RID: 18998 RVA: 0x001126EA File Offset: 0x001108EA
		// (set) Token: 0x06004A37 RID: 18999 RVA: 0x001126FC File Offset: 0x001108FC
		[Parameter(Mandatory = false)]
		public bool ShowHiddenFoldersEnabled
		{
			get
			{
				return (bool)this[Imap4AdConfigurationSchema.ShowHiddenFoldersEnabled];
			}
			set
			{
				this[Imap4AdConfigurationSchema.ShowHiddenFoldersEnabled] = value;
			}
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x00112710 File Offset: 0x00110910
		internal static object ShowHiddenFoldersEnabledGetter(IPropertyBag propertyBag)
		{
			int num = 1;
			ADPropertyDefinition popImapFlags = PopImapAdConfigurationSchema.PopImapFlags;
			return (num & (int)propertyBag[popImapFlags]) != 0;
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x00112740 File Offset: 0x00110940
		internal static void ShowHiddenFoldersEnabledSetter(object value, IPropertyBag propertyBag)
		{
			int num = 1;
			ADPropertyDefinition popImapFlags = PopImapAdConfigurationSchema.PopImapFlags;
			if ((bool)value)
			{
				propertyBag[popImapFlags] = ((int)propertyBag[popImapFlags] | num);
				return;
			}
			propertyBag[popImapFlags] = ((int)propertyBag[popImapFlags] & ~num);
		}

		// Token: 0x0400335A RID: 13146
		private static Imap4AdConfigurationSchema schema = ObjectSchema.GetInstance<Imap4AdConfigurationSchema>();

		// Token: 0x0400335B RID: 13147
		public static string MostDerivedClass = "protocolCfgIMAPServer";
	}
}
