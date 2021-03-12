using System;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000810 RID: 2064
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CalendarAttendee : CalendarPropertyBase
	{
		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x06004CF9 RID: 19705 RVA: 0x0013FAB5 File Offset: 0x0013DCB5
		internal bool IsResponseRequested
		{
			get
			{
				return this.isResponseRequested;
			}
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x06004CFA RID: 19706 RVA: 0x0013FABD File Offset: 0x0013DCBD
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x06004CFB RID: 19707 RVA: 0x0013FAC5 File Offset: 0x0013DCC5
		internal string Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x06004CFC RID: 19708 RVA: 0x0013FACD File Offset: 0x0013DCCD
		internal string ParticipationStatus
		{
			get
			{
				return this.partstat;
			}
		}

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06004CFD RID: 19709 RVA: 0x0013FAD5 File Offset: 0x0013DCD5
		internal string ParticipationRole
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x06004CFE RID: 19710 RVA: 0x0013FADD File Offset: 0x0013DCDD
		internal string CalendarUserType
		{
			get
			{
				return this.calendarUserType;
			}
		}

		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x06004CFF RID: 19711 RVA: 0x0013FAE5 File Offset: 0x0013DCE5
		internal string SentBy
		{
			get
			{
				return this.sentBy;
			}
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x0013FAF0 File Offset: 0x0013DCF0
		protected override bool ProcessParameter(CalendarParameter parameter)
		{
			ParameterId parameterId = parameter.ParameterId;
			if (parameterId <= ParameterId.Delegatee)
			{
				if (parameterId <= ParameterId.CalendarUserType)
				{
					if (parameterId != ParameterId.CommonName)
					{
						if (parameterId == ParameterId.CalendarUserType)
						{
							this.calendarUserType = (string)parameter.Value;
						}
					}
					else
					{
						this.name = CalendarUtil.RemoveDoubleQuotes((string)parameter.Value);
					}
				}
				else if (parameterId != ParameterId.Delegator && parameterId != ParameterId.Delegatee)
				{
				}
			}
			else if (parameterId <= ParameterId.ParticipationRole)
			{
				if (parameterId != ParameterId.ParticipationStatus)
				{
					if (parameterId == ParameterId.ParticipationRole)
					{
						this.role = (string)parameter.Value;
					}
				}
				else
				{
					this.partstat = (string)parameter.Value;
				}
			}
			else if (parameterId != ParameterId.RsvpExpectation)
			{
				if (parameterId == ParameterId.SentBy)
				{
					string text = CalendarUtil.RemoveDoubleQuotes((string)parameter.Value);
					if (text != null)
					{
						text = CalendarUtil.RemoveMailToPrefix(text);
					}
					this.sentBy = text;
				}
			}
			else if (!bool.TryParse(parameter.Value as string, out this.isResponseRequested))
			{
				this.isResponseRequested = true;
			}
			return true;
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x0013FBFC File Offset: 0x0013DDFC
		protected override bool Validate()
		{
			if (!base.Validate())
			{
				return false;
			}
			string text = base.Value as string;
			if (text != null)
			{
				this.address = CalendarUtil.RemoveMailToPrefix(text);
			}
			return this.address.Length > 0;
		}

		// Token: 0x040029F8 RID: 10744
		private string address = string.Empty;

		// Token: 0x040029F9 RID: 10745
		private string sentBy = string.Empty;

		// Token: 0x040029FA RID: 10746
		private string name = string.Empty;

		// Token: 0x040029FB RID: 10747
		private string partstat = string.Empty;

		// Token: 0x040029FC RID: 10748
		private string role = string.Empty;

		// Token: 0x040029FD RID: 10749
		private string calendarUserType = string.Empty;

		// Token: 0x040029FE RID: 10750
		private bool isResponseRequested = true;
	}
}
