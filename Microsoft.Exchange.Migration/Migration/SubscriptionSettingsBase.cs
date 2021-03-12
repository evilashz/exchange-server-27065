using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000094 RID: 148
	internal abstract class SubscriptionSettingsBase : ISubscriptionSettings, IMigrationSerializable
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00025C36 File Offset: 0x00023E36
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x00025C3E File Offset: 0x00023E3E
		public ExDateTime LastModifiedTime { get; protected set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060008A1 RID: 2209
		public abstract PropertyDefinition[] PropertyDefinitions { get; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00025C47 File Offset: 0x00023E47
		protected virtual bool IsEmpty
		{
			get
			{
				return this.LastModifiedTime == ExDateTime.MinValue;
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00025C5C File Offset: 0x00023E5C
		public virtual void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationSubscriptionSettingsLastModifiedTime, (this.LastModifiedTime == ExDateTime.MinValue) ? null : new ExDateTime?(this.LastModifiedTime));
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00025C9C File Offset: 0x00023E9C
		public virtual bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			ExDateTime? exDateTimePropertyOrNull = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationSubscriptionSettingsLastModifiedTime);
			if (exDateTimePropertyOrNull == null)
			{
				this.LastModifiedTime = ExDateTime.MinValue;
			}
			else
			{
				this.LastModifiedTime = exDateTimePropertyOrNull.Value;
			}
			return !this.IsEmpty;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00025CE4 File Offset: 0x00023EE4
		public XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement("SubscriptionSettings");
			if (!argument.HasArgument("verbose"))
			{
				return xelement;
			}
			xelement.Add(new XElement("LastModifiedTime", this.LastModifiedTime));
			this.AddDiagnosticInfoToElement(dataProvider, xelement, argument);
			return xelement;
		}

		// Token: 0x060008A6 RID: 2214
		protected abstract void AddDiagnosticInfoToElement(IMigrationDataProvider dataProvider, XElement parent, MigrationDiagnosticArgument argument);

		// Token: 0x04000359 RID: 857
		internal static readonly PropertyDefinition[] SubscriptionSettingsBasePropertyDefinitions = new PropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationSubscriptionSettingsLastModifiedTime
		};
	}
}
