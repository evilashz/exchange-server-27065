using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Properties.XSO;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x02000231 RID: 561
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class EmailProperty : ContactProperty<string>
	{
		// Token: 0x0600144A RID: 5194 RVA: 0x00049BE8 File Offset: 0x00047DE8
		public EmailProperty(IXSOPropertyManager propertyManager, PropertyDefinition writePropertyDefinition, PropertyDefinition readPropertyDefinition) : base(propertyManager, new PropertyDefinition[]
		{
			writePropertyDefinition,
			readPropertyDefinition
		})
		{
			this.writePropertyDefinition = writePropertyDefinition;
			this.readPropertyDefinition = readPropertyDefinition;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00049C1A File Offset: 0x00047E1A
		public override string ReadProperty(Item item)
		{
			SyncUtilities.ThrowIfArgumentNull("item", item);
			return SyncUtilities.SafeGetProperty<string>(item, this.readPropertyDefinition);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x00049C34 File Offset: 0x00047E34
		public override void WriteProperty(Item item, string desiredValue)
		{
			SyncUtilities.ThrowIfArgumentNull("item", item);
			if (desiredValue != null)
			{
				Participant participant = null;
				if (!base.IsItemNew(item))
				{
					participant = SyncUtilities.SafeGetProperty<Participant>(item, this.writePropertyDefinition);
				}
				Participant value;
				if (participant == null)
				{
					value = new Participant(null, desiredValue, "SMTP");
				}
				else
				{
					value = new Participant(participant.DisplayName, desiredValue, participant.RoutingType);
				}
				item[this.writePropertyDefinition] = value;
			}
		}

		// Token: 0x04000AB5 RID: 2741
		private readonly PropertyDefinition writePropertyDefinition;

		// Token: 0x04000AB6 RID: 2742
		private readonly PropertyDefinition readPropertyDefinition;
	}
}
