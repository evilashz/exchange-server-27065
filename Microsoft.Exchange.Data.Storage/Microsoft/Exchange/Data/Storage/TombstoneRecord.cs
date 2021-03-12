using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000377 RID: 887
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TombstoneRecord
	{
		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x0009CCC2 File Offset: 0x0009AEC2
		// (set) Token: 0x06002726 RID: 10022 RVA: 0x0009CCCA File Offset: 0x0009AECA
		public ExDateTime StartTime { get; set; }

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06002727 RID: 10023 RVA: 0x0009CCD3 File Offset: 0x0009AED3
		// (set) Token: 0x06002728 RID: 10024 RVA: 0x0009CCDB File Offset: 0x0009AEDB
		public ExDateTime EndTime { get; set; }

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x0009CCE4 File Offset: 0x0009AEE4
		// (set) Token: 0x0600272A RID: 10026 RVA: 0x0009CCEC File Offset: 0x0009AEEC
		public byte[] GlobalObjectId { get; set; }

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x0009CCF5 File Offset: 0x0009AEF5
		// (set) Token: 0x0600272C RID: 10028 RVA: 0x0009CCFD File Offset: 0x0009AEFD
		public byte[] UserName { get; set; }

		// Token: 0x0600272D RID: 10029 RVA: 0x0009CD08 File Offset: 0x0009AF08
		public bool TryGetBytes(out byte[] buffer)
		{
			buffer = null;
			bool result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write((int)(this.StartTime.LocalTime.ToFileTimeUtc() / 600000000L));
					binaryWriter.Write((int)(this.EndTime.LocalTime.ToFileTimeUtc() / 600000000L));
					if (this.GlobalObjectId == null)
					{
						ExTraceGlobals.MeetingMessageTracer.TraceError((long)this.GetHashCode(), "Tombstone record GlobalObjectId is null");
						result = false;
					}
					else
					{
						binaryWriter.Write(this.GlobalObjectId.Length);
						binaryWriter.Write(this.GlobalObjectId);
						if (this.UserName == null || this.UserName.Length <= 0)
						{
							ExTraceGlobals.MeetingMessageTracer.TraceError((long)this.GetHashCode(), "Tombstone record UserName is null");
							result = false;
						}
						else
						{
							binaryWriter.Write((short)this.UserName.Length);
							binaryWriter.Write(this.UserName);
							buffer = memoryStream.ToArray();
							result = true;
						}
					}
				}
			}
			return result;
		}
	}
}
