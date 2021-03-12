using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Microsoft.Exchange.Data.Directory.DirSync
{
	// Token: 0x020001B3 RID: 435
	internal class ADDirSyncCookie
	{
		// Token: 0x06001215 RID: 4629 RVA: 0x00057B3D File Offset: 0x00055D3D
		internal ADDirSyncCookie(Guid invocationId, long objectUpdateSequenceNumber, long propertyUpdateSequenceNumber, ADReplicationCursorCollection cursors)
		{
			if (cursors == null)
			{
				throw new ArgumentNullException("cursors");
			}
			this.invocationId = invocationId;
			this.objectUpdateSequenceNumber = objectUpdateSequenceNumber;
			this.propertyUpdateSequenceNumber = propertyUpdateSequenceNumber;
			this.cursors = cursors;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00057B71 File Offset: 0x00055D71
		public Guid InvocationId
		{
			get
			{
				return this.invocationId;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x00057B79 File Offset: 0x00055D79
		public long ObjectUpdateSequenceNumber
		{
			get
			{
				return this.objectUpdateSequenceNumber;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x00057B81 File Offset: 0x00055D81
		public long PropertyUpdateSequenceNumber
		{
			get
			{
				return this.propertyUpdateSequenceNumber;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x00057B89 File Offset: 0x00055D89
		public ADReplicationCursorCollection Cursors
		{
			get
			{
				return this.cursors;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x00057B91 File Offset: 0x00055D91
		public bool MoreData
		{
			get
			{
				return this.objectUpdateSequenceNumber != this.propertyUpdateSequenceNumber;
			}
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00057BA4 File Offset: 0x00055DA4
		public static ADDirSyncCookie Parse(byte[] binaryCookie)
		{
			if (binaryCookie == null)
			{
				throw new ArgumentNullException("binaryCookie");
			}
			Exception innerException = null;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(binaryCookie))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						byte[] first = binaryReader.ReadBytes(4);
						if (first.SequenceEqual(ADDirSyncCookie.Header) && binaryReader.ReadInt32() == 3)
						{
							binaryReader.ReadInt64();
							binaryReader.ReadInt64();
							int num = binaryReader.ReadInt32();
							long num2 = binaryReader.ReadInt64();
							binaryReader.ReadInt64();
							long num3 = binaryReader.ReadInt64();
							Guid guid = new Guid(binaryReader.ReadBytes(16));
							byte[] binary = binaryReader.ReadBytes(num);
							ADReplicationCursorCollection adreplicationCursorCollection = (num == 0) ? new ADReplicationCursorCollection() : ADReplicationCursorCollection.Parse(binary);
							return new ADDirSyncCookie(guid, num2, num3, adreplicationCursorCollection);
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
			throw new FormatException("Unrecognized cookie format.", innerException);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00057CC4 File Offset: 0x00055EC4
		public byte[] ToByteArray()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					byte[] array = (this.Cursors.Count == 0) ? new byte[0] : this.Cursors.ToByteArray(1);
					binaryWriter.Write(ADDirSyncCookie.Header);
					binaryWriter.Write(3);
					binaryWriter.Write(0L);
					binaryWriter.Write(0L);
					binaryWriter.Write(array.Length);
					binaryWriter.Write(this.ObjectUpdateSequenceNumber);
					binaryWriter.Write(0L);
					binaryWriter.Write(this.PropertyUpdateSequenceNumber);
					binaryWriter.Write(this.InvocationId.ToByteArray());
					binaryWriter.Write(array);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00057DA4 File Offset: 0x00055FA4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "InvocationId={0} ObjectUsn={1} PropertyUsn={2} MoreData={3} Cursors={{{4}}}", new object[]
			{
				this.InvocationId,
				this.ObjectUpdateSequenceNumber,
				this.PropertyUpdateSequenceNumber,
				this.MoreData,
				this.Cursors
			});
		}

		// Token: 0x04000A80 RID: 2688
		private const int Version = 3;

		// Token: 0x04000A81 RID: 2689
		private static readonly byte[] Header = new byte[]
		{
			77,
			83,
			68,
			83
		};

		// Token: 0x04000A82 RID: 2690
		private readonly Guid invocationId;

		// Token: 0x04000A83 RID: 2691
		private readonly long objectUpdateSequenceNumber;

		// Token: 0x04000A84 RID: 2692
		private readonly long propertyUpdateSequenceNumber;

		// Token: 0x04000A85 RID: 2693
		private readonly ADReplicationCursorCollection cursors;
	}
}
