using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.DirSync
{
	// Token: 0x020001B7 RID: 439
	internal class ADReplicationCursorCollection : KeyedCollection<Guid, ReplicationCursor>
	{
		// Token: 0x06001239 RID: 4665 RVA: 0x000580B5 File Offset: 0x000562B5
		public ADReplicationCursorCollection()
		{
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x000580C0 File Offset: 0x000562C0
		public ADReplicationCursorCollection(IEnumerable<ReplicationCursor> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			foreach (ReplicationCursor item in collection)
			{
				base.Add(item);
			}
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0005811C File Offset: 0x0005631C
		public static ADReplicationCursorCollection Parse(byte[] binary)
		{
			if (binary == null)
			{
				throw new ArgumentNullException("binary");
			}
			ADReplicationCursorCollection adreplicationCursorCollection = new ADReplicationCursorCollection();
			Exception innerException = null;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(binary))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						int num = binaryReader.ReadInt32();
						if (num == 1)
						{
							binaryReader.ReadInt32();
							int num2 = binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							while (num2-- > 0)
							{
								Guid sourceInvocationId = new Guid(binaryReader.ReadBytes(16));
								long upToDatenessUsn = binaryReader.ReadInt64();
								adreplicationCursorCollection.Add(new ReplicationCursor(sourceInvocationId, upToDatenessUsn, DateTime.MinValue, null));
							}
							return adreplicationCursorCollection;
						}
					}
				}
			}
			catch (ArgumentException ex)
			{
				innerException = ex;
			}
			catch (IOException ex2)
			{
				innerException = ex2;
			}
			throw new FormatException("Unrecognized format.", innerException);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00058218 File Offset: 0x00056418
		public byte[] ToByteArray(int version)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					if (version == 1)
					{
						binaryWriter.Write(version);
						binaryWriter.Write(0);
						binaryWriter.Write(base.Count);
						binaryWriter.Write(0);
						foreach (ReplicationCursor replicationCursor in this)
						{
							binaryWriter.Write(replicationCursor.SourceInvocationId.ToByteArray());
							binaryWriter.Write(replicationCursor.UpToDatenessUsn);
						}
						return memoryStream.ToArray();
					}
				}
			}
			throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Version '{0}' is not supported.", new object[]
			{
				version
			}));
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00058314 File Offset: 0x00056514
		public long GetNextUpdateSequenceNumber(Guid invocationId)
		{
			if (!base.Contains(invocationId))
			{
				return 0L;
			}
			return 1L + base[invocationId].UpToDatenessUsn;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00058334 File Offset: 0x00056534
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ReplicationCursor replicationCursor in this)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(replicationCursor.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x000583A4 File Offset: 0x000565A4
		protected override Guid GetKeyForItem(ReplicationCursor item)
		{
			return item.SourceInvocationId;
		}
	}
}
