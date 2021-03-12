using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Nspi.Rfri;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000044 RID: 68
	internal class ProtocolLogSession
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x00011DEA File Offset: 0x0000FFEA
		internal ProtocolLogSession(LogRowFormatter row)
		{
			this.row = row;
			this[ProtocolLog.Field.SequenceNumber] = 0;
		}

		// Token: 0x170000C3 RID: 195
		internal object this[ProtocolLog.Field field]
		{
			get
			{
				if (this.row != null)
				{
					return this.row[(int)field];
				}
				return null;
			}
			set
			{
				if (this.row != null)
				{
					this.row[(int)field] = value;
				}
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00011E38 File Offset: 0x00010038
		internal void Append(string operation, NspiStatus status, int queuedTime, int processingTime)
		{
			if (ProtocolLog.Enabled)
			{
				this.Append(operation, (status == NspiStatus.Success || status == NspiStatus.UnbindSuccess) ? null : string.Format(NumberFormatInfo.InvariantInfo, "{0:X}", new object[]
				{
					(int)status
				}), queuedTime, processingTime);
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00011E80 File Offset: 0x00010080
		internal void Append(string operation, RfriStatus status, int queuedTime, int processingTime)
		{
			if (ProtocolLog.Enabled)
			{
				this.Append(operation, (status == RfriStatus.Success) ? null : string.Format(NumberFormatInfo.InvariantInfo, "{0:X}", new object[]
				{
					(int)status
				}), queuedTime, processingTime);
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00011EC4 File Offset: 0x000100C4
		internal void AppendProtocolFailure(string operation, string operationSpecific, string failure)
		{
			if (ProtocolLog.Enabled)
			{
				this[ProtocolLog.Field.OperationSpecific] = operationSpecific;
				this[ProtocolLog.Field.Failures] = failure;
				this.Append(operation, string.Empty, 0, 0);
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00011EF0 File Offset: 0x000100F0
		private void Append(string operation, string status, int queuedTime, int processingTime)
		{
			if (ProtocolLog.Enabled)
			{
				this[ProtocolLog.Field.Operation] = operation;
				this[ProtocolLog.Field.RpcStatus] = status;
				this[ProtocolLog.Field.ProcessingTime] = processingTime;
				this[ProtocolLog.Field.Delay] = ((queuedTime == 0) ? null : queuedTime);
				ProtocolLog.Append(this.row);
				this[ProtocolLog.Field.SequenceNumber] = (int)this[ProtocolLog.Field.SequenceNumber] + 1;
				this[ProtocolLog.Field.OperationSpecific] = null;
				this[ProtocolLog.Field.Failures] = null;
				this[ProtocolLog.Field.Authentication] = null;
			}
		}

		// Token: 0x040001A6 RID: 422
		private readonly LogRowFormatter row;
	}
}
