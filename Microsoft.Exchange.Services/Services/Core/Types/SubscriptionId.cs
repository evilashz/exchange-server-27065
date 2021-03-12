using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000899 RID: 2201
	internal class SubscriptionId
	{
		// Token: 0x06003EDA RID: 16090 RVA: 0x000D99C8 File Offset: 0x000D7BC8
		public SubscriptionId(Guid subscriptionGuid) : this(subscriptionGuid, LocalServer.GetServer().Fqdn)
		{
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x000D99DB File Offset: 0x000D7BDB
		public SubscriptionId(Guid subscriptionGuid, string serverFQDN)
		{
			this.mailboxGuid = Guid.Empty;
			base..ctor();
			this.serverFQDN = serverFQDN;
			this.subscriptionGuid = subscriptionGuid;
			this.timeCreated = ExDateTime.UtcNow;
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x000D9A07 File Offset: 0x000D7C07
		public SubscriptionId(Guid subscriptionGuid, Guid mailboxGuid) : this(subscriptionGuid)
		{
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x000D9A17 File Offset: 0x000D7C17
		private SubscriptionId(Guid subscriptionGuid, string serverFQDN, ExDateTime timeCreated, Guid mailboxGuid)
		{
			this.mailboxGuid = Guid.Empty;
			base..ctor();
			this.subscriptionGuid = subscriptionGuid;
			this.serverFQDN = serverFQDN;
			this.timeCreated = timeCreated;
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x000D9A48 File Offset: 0x000D7C48
		public static SubscriptionId Parse(string idAndCASString)
		{
			SubscriptionId result;
			try
			{
				byte[] buffer = Convert.FromBase64String(idAndCASString);
				using (MemoryStream memoryStream = new MemoryStream(buffer))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						ushort num = binaryReader.ReadUInt16();
						if (num > 2048)
						{
							ExTraceGlobals.SubscriptionsTracer.TraceDebug(0L, "[SubscriptionId::Parse] FQDN length in id exceeded limits.");
							throw new InvalidSubscriptionException();
						}
						if (memoryStream.Length != (long)((int)num + SubscriptionId.GuidSize + 2 + 4 + 8) && memoryStream.Length != (long)((int)num + SubscriptionId.GuidSize + SubscriptionId.GuidSize + 2 + 4 + 4 + 8))
						{
							ExTraceGlobals.SubscriptionsTracer.TraceDebug(0L, "[SubscriptionId::Parse] Id buffer had an unexcepted length.");
							throw new InvalidSubscriptionException();
						}
						byte[] array = binaryReader.ReadBytes((int)num);
						string @string = CTSGlobals.AsciiEncoding.GetString(array, 0, array.Length);
						int count = binaryReader.ReadInt32();
						byte[] b = binaryReader.ReadBytes(count);
						ExDateTime exDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, binaryReader.ReadInt64());
						Guid empty = Guid.Empty;
						if (binaryReader.PeekChar() != -1)
						{
							int count2 = binaryReader.ReadInt32();
							byte[] b2 = binaryReader.ReadBytes(count2);
							empty = new Guid(b2);
						}
						result = new SubscriptionId(new Guid(b), @string, exDateTime, empty);
					}
				}
			}
			catch (EndOfStreamException innerException)
			{
				throw new InvalidSubscriptionException(innerException);
			}
			catch (ArgumentOutOfRangeException innerException2)
			{
				throw new InvalidSubscriptionException(innerException2);
			}
			catch (FormatException innerException3)
			{
				ExTraceGlobals.SubscriptionsTracer.TraceDebug(0L, "[SubscriptionId::Parse] subscription id was not a valid guid or was not valid base64");
				throw new InvalidSubscriptionException(innerException3);
			}
			return result;
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06003EDF RID: 16095 RVA: 0x000D9C20 File Offset: 0x000D7E20
		public Guid Id
		{
			get
			{
				return this.subscriptionGuid;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06003EE0 RID: 16096 RVA: 0x000D9C28 File Offset: 0x000D7E28
		public string ServerFQDN
		{
			get
			{
				return this.serverFQDN;
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06003EE1 RID: 16097 RVA: 0x000D9C30 File Offset: 0x000D7E30
		public ExDateTime TimeCreated
		{
			get
			{
				return this.timeCreated;
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06003EE2 RID: 16098 RVA: 0x000D9C38 File Offset: 0x000D7E38
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x000D9C40 File Offset: 0x000D7E40
		public override string ToString()
		{
			byte[] bytes = CTSGlobals.AsciiEncoding.GetBytes(this.serverFQDN.ToLower());
			byte[] array = this.subscriptionGuid.ToByteArray();
			long utcTicks = this.timeCreated.UtcTicks;
			byte[] array2 = this.mailboxGuid.ToByteArray();
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write((ushort)bytes.Length);
					binaryWriter.Write(bytes);
					binaryWriter.Write(array.Length);
					binaryWriter.Write(array);
					binaryWriter.Write(utcTicks);
					binaryWriter.Write(array2.Length);
					binaryWriter.Write(array2);
					binaryWriter.Flush();
					result = Convert.ToBase64String(memoryStream.ToArray(), 0, (int)memoryStream.Length);
				}
			}
			return result;
		}

		// Token: 0x0400240D RID: 9229
		private const short MaxFqdnLength = 2048;

		// Token: 0x0400240E RID: 9230
		private static readonly int GuidSize = Marshal.SizeOf(typeof(Guid));

		// Token: 0x0400240F RID: 9231
		private string serverFQDN;

		// Token: 0x04002410 RID: 9232
		private Guid subscriptionGuid;

		// Token: 0x04002411 RID: 9233
		private ExDateTime timeCreated;

		// Token: 0x04002412 RID: 9234
		private Guid mailboxGuid;
	}
}
