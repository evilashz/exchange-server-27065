using System;
using System.Text;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Common.Sharing;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000217 RID: 535
	internal class XsoEmailSubjectProperty : XsoStringProperty
	{
		// Token: 0x06001482 RID: 5250 RVA: 0x0007693A File Offset: 0x00074B3A
		public XsoEmailSubjectProperty(PropertyType propertyType = PropertyType.ReadOnly) : base(ItemSchema.Subject, propertyType)
		{
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00076948 File Offset: 0x00074B48
		public override string StringData
		{
			get
			{
				string result = base.StringData;
				if (this.IsItemDelegated())
				{
					MeetingMessage meetingMessage = base.XsoItem as MeetingMessage;
					Participant receivedRepresenting = meetingMessage.ReceivedRepresenting;
					if (receivedRepresenting != null)
					{
						try
						{
							using (CalendarItemBase correlatedItem = meetingMessage.GetCorrelatedItem())
							{
								if (correlatedItem != null && correlatedItem.IsCancelled)
								{
									result = this.BuildDelegatedSubjectLine(Strings.CanceledDelegatedSubjectPrefix(receivedRepresenting.DisplayName).ToString(Command.CurrentCommand.MailboxSession.PreferedCulture), base.StringData);
								}
								else
								{
									result = this.BuildDelegatedSubjectLine(Strings.DelegatedSubjectPrefix(receivedRepresenting.DisplayName).ToString(Command.CurrentCommand.MailboxSession.PreferedCulture), base.StringData);
								}
							}
						}
						catch (StoragePermanentException)
						{
							result = this.BuildDelegatedSubjectLine(Strings.DelegatedSubjectPrefix(receivedRepresenting.DisplayName).ToString(Command.CurrentCommand.MailboxSession.PreferedCulture), base.StringData);
						}
						catch (StorageTransientException)
						{
							result = this.BuildDelegatedSubjectLine(Strings.DelegatedSubjectPrefix(receivedRepresenting.DisplayName).ToString(Command.CurrentCommand.MailboxSession.PreferedCulture), base.StringData);
						}
						catch (ADUserNotFoundException)
						{
							result = this.BuildDelegatedSubjectLine(Strings.DelegatedSubjectPrefix(receivedRepresenting.DisplayName).ToString(Command.CurrentCommand.MailboxSession.PreferedCulture), base.StringData);
						}
					}
				}
				return result;
			}
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00076ADC File Offset: 0x00074CDC
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			if (Command.CurrentCommand != null && Command.CurrentCommand.Request.Version >= 160)
			{
				base.XsoItem[base.PropertyDef] = string.Empty;
				return;
			}
			base.InternalSetToDefault(srcProperty);
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00076B1C File Offset: 0x00074D1C
		private string BuildDelegatedSubjectLine(string prefix, string subject)
		{
			StringBuilder stringBuilder = new StringBuilder(prefix);
			stringBuilder.Append(" ");
			stringBuilder.Append(subject);
			return stringBuilder.ToString();
		}
	}
}
