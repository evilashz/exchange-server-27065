using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200046B RID: 1131
	[Serializable]
	public sealed class MailboxAssociationReplicationStatePresentationObject : ConfigurableObject
	{
		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x0009DCA9 File Offset: 0x0009BEA9
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MailboxAssociationReplicationStatePresentationObject.schema;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x0009DCB0 File Offset: 0x0009BEB0
		// (set) Token: 0x060027FE RID: 10238 RVA: 0x0009DCB8 File Offset: 0x0009BEB8
		public new ObjectId Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				this[SimpleProviderObjectSchema.Identity] = value;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060027FF RID: 10239 RVA: 0x0009DCC6 File Offset: 0x0009BEC6
		// (set) Token: 0x06002800 RID: 10240 RVA: 0x0009DCD8 File Offset: 0x0009BED8
		[Parameter(Mandatory = false)]
		public ExDateTime? NextReplicationTime
		{
			get
			{
				return (ExDateTime?)this[MailboxAssociationReplicationStatePresentationObject.MailboxAssociationReplicationStatePresentationObjectSchema.NextReplicationTime];
			}
			set
			{
				this[MailboxAssociationReplicationStatePresentationObject.MailboxAssociationReplicationStatePresentationObjectSchema.NextReplicationTime] = value;
			}
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x0009DCEB File Offset: 0x0009BEEB
		public MailboxAssociationReplicationStatePresentationObject() : base(new SimpleProviderPropertyBag())
		{
			base.SetExchangeVersion(ExchangeObjectVersion.Current);
			base.ResetChangeTracking();
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x0009DD0C File Offset: 0x0009BF0C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("Mailbox=");
			stringBuilder.Append(this.Identity);
			stringBuilder.Append(", NextReplicationTime=");
			stringBuilder.Append(this.NextReplicationTime);
			return stringBuilder.ToString();
		}

		// Token: 0x04001DCD RID: 7629
		private static MailboxAssociationReplicationStatePresentationObject.MailboxAssociationReplicationStatePresentationObjectSchema schema = ObjectSchema.GetInstance<MailboxAssociationReplicationStatePresentationObject.MailboxAssociationReplicationStatePresentationObjectSchema>();

		// Token: 0x0200046C RID: 1132
		private class MailboxAssociationReplicationStatePresentationObjectSchema : SimpleProviderObjectSchema
		{
			// Token: 0x04001DCE RID: 7630
			public static readonly SimpleProviderPropertyDefinition NextReplicationTime = new SimpleProviderPropertyDefinition("NextReplicationTime", ExchangeObjectVersion.Current, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}
