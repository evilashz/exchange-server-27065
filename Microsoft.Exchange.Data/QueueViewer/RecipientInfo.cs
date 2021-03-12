using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics.Components.Data;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x0200028C RID: 652
	[Serializable]
	public class RecipientInfo
	{
		// Token: 0x06001792 RID: 6034 RVA: 0x00049E0D File Offset: 0x0004800D
		internal RecipientInfo()
		{
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00049E18 File Offset: 0x00048018
		private RecipientInfo(PropertyStreamReader reader)
		{
			KeyValuePair<string, object> item;
			reader.Read(out item);
			if (!string.Equals("NumProperties", item.Key, StringComparison.OrdinalIgnoreCase))
			{
				throw new SerializationException(string.Format("Cannot deserialize RecipientInfo. Expected property NumProperties, but found property '{0}'", item.Key));
			}
			int value = PropertyStreamReader.GetValue<int>(item);
			for (int i = 0; i < value; i++)
			{
				reader.Read(out item);
				if (string.Equals("Address", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.address = PropertyStreamReader.GetValue<string>(item);
				}
				else if (string.Equals("Status", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.status = (RecipientStatus)PropertyStreamReader.GetValue<int>(item);
				}
				else if (string.Equals("LastError", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.lastError = PropertyStreamReader.GetValue<string>(item);
				}
				else if (string.Equals("Type", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.type = (MailRecipientType)PropertyStreamReader.GetValue<int>(item);
				}
				else if (string.Equals("LastErrorCode", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.lastErrorCode = PropertyStreamReader.GetValue<int>(item);
				}
				else if (string.Equals("FinalDestination", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.finalDestination = PropertyStreamReader.GetValue<string>(item);
				}
				else if (string.Equals("OutboundIPPool", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.outboundIPPool = PropertyStreamReader.GetValue<int>(item);
				}
				else
				{
					ExTraceGlobals.SerializationTracer.TraceWarning<string>(0L, "Ignoring unknown property '{0} in recipientInfo", item.Key);
				}
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x00049F90 File Offset: 0x00048190
		// (set) Token: 0x06001795 RID: 6037 RVA: 0x00049F98 File Offset: 0x00048198
		public string Address
		{
			get
			{
				return this.address;
			}
			internal set
			{
				this.address = value;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x00049FA1 File Offset: 0x000481A1
		// (set) Token: 0x06001797 RID: 6039 RVA: 0x00049FA9 File Offset: 0x000481A9
		public int OutboundIPPool
		{
			get
			{
				return this.outboundIPPool;
			}
			internal set
			{
				this.outboundIPPool = value;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x00049FB2 File Offset: 0x000481B2
		// (set) Token: 0x06001799 RID: 6041 RVA: 0x00049FBA File Offset: 0x000481BA
		public MailRecipientType Type
		{
			get
			{
				return this.type;
			}
			internal set
			{
				this.type = value;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x00049FC3 File Offset: 0x000481C3
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x00049FCB File Offset: 0x000481CB
		public string FinalDestination
		{
			get
			{
				return this.finalDestination;
			}
			internal set
			{
				this.finalDestination = value;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x00049FD4 File Offset: 0x000481D4
		// (set) Token: 0x0600179D RID: 6045 RVA: 0x00049FDC File Offset: 0x000481DC
		public RecipientStatus Status
		{
			get
			{
				return this.status;
			}
			internal set
			{
				this.status = value;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x00049FE5 File Offset: 0x000481E5
		// (set) Token: 0x0600179F RID: 6047 RVA: 0x00049FED File Offset: 0x000481ED
		internal int LastErrorCode
		{
			get
			{
				return this.lastErrorCode;
			}
			set
			{
				this.lastErrorCode = value;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x00049FF6 File Offset: 0x000481F6
		// (set) Token: 0x060017A1 RID: 6049 RVA: 0x0004A01C File Offset: 0x0004821C
		public string LastError
		{
			get
			{
				if (this.lastError != null)
				{
					return this.lastError;
				}
				if (this.lastErrorCode != 0)
				{
					return StatusCodeConverter.UnreachableReasonToString((UnreachableReason)this.lastErrorCode);
				}
				return null;
			}
			internal set
			{
				this.lastError = value;
			}
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0004A028 File Offset: 0x00048228
		public override string ToString()
		{
			return string.Format("{0};{1};{2};{3};{4};{5};{6}", new object[]
			{
				this.address,
				(int)this.status,
				(int)this.type,
				this.lastError,
				this.lastErrorCode,
				this.finalDestination,
				this.outboundIPPool
			});
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0004A09A File Offset: 0x0004829A
		internal static RecipientInfo Create(PropertyStreamReader reader)
		{
			return new RecipientInfo(reader);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0004A0A4 File Offset: 0x000482A4
		internal void ToByteArray(ref byte[] bytes, ref int offset)
		{
			int num = 6 + ((this.OutboundIPPool > 0) ? 1 : 0);
			int num2 = 0;
			PropertyStreamWriter.WritePropertyKeyValue("NumProperties", StreamPropertyType.Int32, num, ref bytes, ref offset);
			PropertyStreamWriter.WritePropertyKeyValue("Address", StreamPropertyType.String, this.address, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue("Status", StreamPropertyType.Int32, (int)this.Status, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue("Type", StreamPropertyType.Int32, (int)this.Type, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue("LastError", StreamPropertyType.String, this.LastError, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue("LastErrorCode", StreamPropertyType.Int32, this.LastErrorCode, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue("FinalDestination", StreamPropertyType.String, this.FinalDestination, ref bytes, ref offset);
			num2++;
			if (this.outboundIPPool > 0)
			{
				PropertyStreamWriter.WritePropertyKeyValue("OutboundIPPool", StreamPropertyType.Int32, this.outboundIPPool, ref bytes, ref offset);
				num2++;
			}
		}

		// Token: 0x04000DB7 RID: 3511
		private const string NumPropertiesKey = "NumProperties";

		// Token: 0x04000DB8 RID: 3512
		private const string AddressKey = "Address";

		// Token: 0x04000DB9 RID: 3513
		private const string StatusKey = "Status";

		// Token: 0x04000DBA RID: 3514
		private const string TypeKey = "Type";

		// Token: 0x04000DBB RID: 3515
		private const string LastErrorKey = "LastError";

		// Token: 0x04000DBC RID: 3516
		private const string LastErrorCodeKey = "LastErrorCode";

		// Token: 0x04000DBD RID: 3517
		private const string FinalDestinationKey = "FinalDestination";

		// Token: 0x04000DBE RID: 3518
		private const string OutboundIPPoolKey = "OutboundIPPool";

		// Token: 0x04000DBF RID: 3519
		private string address;

		// Token: 0x04000DC0 RID: 3520
		private RecipientStatus status;

		// Token: 0x04000DC1 RID: 3521
		private MailRecipientType type;

		// Token: 0x04000DC2 RID: 3522
		private string finalDestination;

		// Token: 0x04000DC3 RID: 3523
		private string lastError;

		// Token: 0x04000DC4 RID: 3524
		private int lastErrorCode;

		// Token: 0x04000DC5 RID: 3525
		private int outboundIPPool;
	}
}
