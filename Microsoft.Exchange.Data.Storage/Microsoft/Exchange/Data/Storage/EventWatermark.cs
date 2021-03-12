using System;
using System.IO;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000707 RID: 1799
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EventWatermark
	{
		// Token: 0x06004740 RID: 18240 RVA: 0x0012F308 File Offset: 0x0012D508
		internal EventWatermark(Guid mdbGuid, long mapiWatermark, bool wasEventProcessed)
		{
			this.mdbGuid = mdbGuid;
			this.mapiWatermark = mapiWatermark;
			this.wasEventProcessed = wasEventProcessed;
		}

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x06004741 RID: 18241 RVA: 0x0012F325 File Offset: 0x0012D525
		internal long MapiWatermark
		{
			get
			{
				return this.mapiWatermark;
			}
		}

		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x06004742 RID: 18242 RVA: 0x0012F32D File Offset: 0x0012D52D
		internal bool WasEventProcessed
		{
			get
			{
				return this.wasEventProcessed;
			}
		}

		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x06004743 RID: 18243 RVA: 0x0012F335 File Offset: 0x0012D535
		internal Guid MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x0012F340 File Offset: 0x0012D540
		public static EventWatermark Deserialize(string base64String)
		{
			if (base64String == null)
			{
				throw new ArgumentNullException(base64String);
			}
			byte[] buffer = null;
			try
			{
				buffer = Convert.FromBase64String(base64String);
			}
			catch (FormatException innerException)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidBase64StringFormat(base64String), innerException);
			}
			EventWatermark result;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(buffer))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						int num = binaryReader.ReadInt32();
						Guid empty;
						if (num == 0)
						{
							empty = Guid.Empty;
						}
						else
						{
							if (num != 1)
							{
								throw new CorruptDataException(ServerStrings.ExInvalidWatermarkString);
							}
							empty = new Guid(binaryReader.ReadBytes(16));
						}
						long num2 = binaryReader.ReadInt64();
						bool flag = binaryReader.ReadBoolean();
						result = new EventWatermark(empty, num2, flag);
					}
				}
			}
			catch (IOException innerException2)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidWatermarkString, innerException2);
			}
			return result;
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x0012F42C File Offset: 0x0012D62C
		public string ToBase64String()
		{
			int num = 29;
			byte[] array = new byte[num];
			int num2 = 0;
			num2 += ExBitConverter.Write(1, array, num2);
			num2 += ExBitConverter.Write(this.mdbGuid, array, num2);
			num2 += ExBitConverter.Write(this.mapiWatermark, array, num2);
			array[num2++] = (this.wasEventProcessed ? 1 : 0);
			return Convert.ToBase64String(array);
		}

		// Token: 0x040026F9 RID: 9977
		private const int SizeOfGuidByteArray = 16;

		// Token: 0x040026FA RID: 9978
		private readonly long mapiWatermark;

		// Token: 0x040026FB RID: 9979
		private readonly bool wasEventProcessed;

		// Token: 0x040026FC RID: 9980
		private readonly Guid mdbGuid;
	}
}
