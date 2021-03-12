using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000094 RID: 148
	internal class LogTransactionInformationCollector
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x0001DEB4 File Offset: 0x0001C0B4
		public void AddLogTransactionInformationBlock(ILogTransactionInformation logTransactionInformationBlock)
		{
			bool flag = false;
			switch (logTransactionInformationBlock.Type())
			{
			case 2:
				if (this.logTransactionInformationIdentityBlock == null)
				{
					this.logTransactionInformationIdentityBlock = logTransactionInformationBlock;
				}
				break;
			case 3:
				if (this.logTransactionInformationOperationBlock == null)
				{
					this.logTransactionInformationOperationBlock = logTransactionInformationBlock;
				}
				break;
			case 4:
				if (this.logTransactionInformationOperationBlock == null)
				{
					this.logTransactionInformationOperationBlock = logTransactionInformationBlock;
				}
				break;
			case 5:
				if (this.logTransactionInformationOperationBlock == null)
				{
					this.logTransactionInformationOperationBlock = logTransactionInformationBlock;
				}
				break;
			default:
			{
				LogTransactionInformationCollector.Counter counter;
				if (!this.perLogTransactionInformationBlockTypeCounter.TryGetValue(logTransactionInformationBlock.Type(), out counter))
				{
					counter = new LogTransactionInformationCollector.Counter
					{
						Value = 1
					};
					this.perLogTransactionInformationBlockTypeCounter.Add(logTransactionInformationBlock.Type(), counter);
				}
				else
				{
					counter.Value++;
				}
				flag = true;
				break;
			}
			}
			int num = logTransactionInformationBlock.Serialize(null, 0);
			if (LogTransactionInformationCollector.AvailableBufferLength <= this.usedBufferLength + num)
			{
				flag = false;
				this.shouldComputeDigest = true;
			}
			else
			{
				this.usedBufferLength += num;
			}
			if (flag)
			{
				this.logTransactionInformationList.AddLast(logTransactionInformationBlock);
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001DFB8 File Offset: 0x0001C1B8
		public int Serialize(byte[] buffer, int offset)
		{
			int num = offset;
			if (buffer != null)
			{
				buffer[offset] = LogTransactionInformationCollector.Version;
			}
			offset++;
			if (this.shouldComputeDigest)
			{
				int num2 = offset;
				if (this.logTransactionInformationIdentityBlock != null)
				{
					num2 += this.logTransactionInformationIdentityBlock.Serialize(null, num2);
					if (num2 > LogTransactionInformationCollector.AvailableBufferLength)
					{
						return offset - num;
					}
					if (buffer != null)
					{
						offset += this.logTransactionInformationIdentityBlock.Serialize(buffer, offset);
					}
					else
					{
						offset = num2;
					}
				}
				if (this.logTransactionInformationOperationBlock != null)
				{
					num2 += this.logTransactionInformationOperationBlock.Serialize(null, num2);
					if (num2 > LogTransactionInformationCollector.AvailableBufferLength)
					{
						return offset - num;
					}
					if (buffer != null)
					{
						offset += this.logTransactionInformationOperationBlock.Serialize(buffer, offset);
					}
					else
					{
						offset = num2;
					}
				}
				ILogTransactionInformation logTransactionInformation = new LogTransactionInformationDigest(this.perLogTransactionInformationBlockTypeCounter);
				num2 += logTransactionInformation.Serialize(null, num2);
				if (num2 > LogTransactionInformationCollector.AvailableBufferLength)
				{
					return offset - num;
				}
				if (buffer != null)
				{
					offset += logTransactionInformation.Serialize(buffer, offset);
				}
				else
				{
					offset = num2;
				}
				using (LinkedList<ILogTransactionInformation>.Enumerator enumerator = this.logTransactionInformationList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ILogTransactionInformation logTransactionInformation2 = enumerator.Current;
						num2 += logTransactionInformation2.Serialize(null, num2);
						if (num2 > LogTransactionInformationCollector.AvailableBufferLength)
						{
							return offset - num;
						}
						if (buffer != null)
						{
							offset += logTransactionInformation2.Serialize(buffer, offset);
						}
						else
						{
							offset = num2;
						}
					}
					goto IL_1A1;
				}
			}
			if (this.logTransactionInformationIdentityBlock != null)
			{
				offset += this.logTransactionInformationIdentityBlock.Serialize(buffer, offset);
			}
			if (this.logTransactionInformationOperationBlock != null)
			{
				offset += this.logTransactionInformationOperationBlock.Serialize(buffer, offset);
			}
			foreach (ILogTransactionInformation logTransactionInformation3 in this.logTransactionInformationList)
			{
				offset += logTransactionInformation3.Serialize(buffer, offset);
			}
			IL_1A1:
			return offset - num;
		}

		// Token: 0x040003DF RID: 991
		public static readonly byte Version = 0;

		// Token: 0x040003E0 RID: 992
		public static readonly int AvailableBufferLength = 70;

		// Token: 0x040003E1 RID: 993
		private bool shouldComputeDigest;

		// Token: 0x040003E2 RID: 994
		private ILogTransactionInformation logTransactionInformationIdentityBlock;

		// Token: 0x040003E3 RID: 995
		private ILogTransactionInformation logTransactionInformationOperationBlock;

		// Token: 0x040003E4 RID: 996
		private LinkedList<ILogTransactionInformation> logTransactionInformationList = new LinkedList<ILogTransactionInformation>();

		// Token: 0x040003E5 RID: 997
		private int usedBufferLength;

		// Token: 0x040003E6 RID: 998
		private Dictionary<byte, LogTransactionInformationCollector.Counter> perLogTransactionInformationBlockTypeCounter = new Dictionary<byte, LogTransactionInformationCollector.Counter>();

		// Token: 0x02000095 RID: 149
		internal class Counter
		{
			// Token: 0x1700015C RID: 348
			// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001E197 File Offset: 0x0001C397
			// (set) Token: 0x06000531 RID: 1329 RVA: 0x0001E19F File Offset: 0x0001C39F
			public int Value { get; set; }
		}
	}
}
