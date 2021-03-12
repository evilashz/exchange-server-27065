using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200016C RID: 364
	internal sealed class LinkMetadata
	{
		// Token: 0x06000C15 RID: 3093 RVA: 0x000257A5 File Offset: 0x000239A5
		private LinkMetadata()
		{
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x000257AD File Offset: 0x000239AD
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x000257B5 File Offset: 0x000239B5
		public string AttributeName { get; private set; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x000257BE File Offset: 0x000239BE
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x000257C6 File Offset: 0x000239C6
		public string TargetDistinguishedName { get; private set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x000257CF File Offset: 0x000239CF
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x000257D7 File Offset: 0x000239D7
		public DateTime CreationTime { get; private set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x000257E0 File Offset: 0x000239E0
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x000257E8 File Offset: 0x000239E8
		public DateTime DeletionTime { get; private set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x000257F1 File Offset: 0x000239F1
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x000257F9 File Offset: 0x000239F9
		public int Version { get; private set; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x00025802 File Offset: 0x00023A02
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x0002580A File Offset: 0x00023A0A
		public DateTime LastWriteTime { get; private set; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x00025813 File Offset: 0x00023A13
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x0002581B File Offset: 0x00023A1B
		public Guid OriginatingInvocationId { get; private set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00025824 File Offset: 0x00023A24
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x0002582C File Offset: 0x00023A2C
		public long OriginatingUpdateSequenceNumber { get; private set; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00025835 File Offset: 0x00023A35
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x0002583D File Offset: 0x00023A3D
		public long LocalUpdateSequenceNumber { get; private set; }

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00025846 File Offset: 0x00023A46
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x0002584E File Offset: 0x00023A4E
		public byte[] Data { get; private set; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00025857 File Offset: 0x00023A57
		public bool IsDeleted
		{
			get
			{
				return this.DeletionTime != DateTime.MinValue;
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002586C File Offset: 0x00023A6C
		public static LinkMetadata Parse(byte[] binary)
		{
			if (binary == null)
			{
				throw new ArgumentNullException("binary");
			}
			Exception innerException;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(binary))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.Unicode))
					{
						LinkMetadata linkMetadata = new LinkMetadata();
						int num = binaryReader.ReadInt32();
						int num2 = binaryReader.ReadInt32();
						int count = binaryReader.ReadInt32();
						int num3 = binaryReader.ReadInt32();
						linkMetadata.DeletionTime = AttributeMetadata.ReadFileTimeUtc(binaryReader);
						linkMetadata.CreationTime = AttributeMetadata.ReadFileTimeUtc(binaryReader);
						linkMetadata.Version = binaryReader.ReadInt32();
						linkMetadata.LastWriteTime = AttributeMetadata.ReadFileTimeUtc(binaryReader);
						linkMetadata.OriginatingInvocationId = new Guid(binaryReader.ReadBytes(16));
						binaryReader.ReadInt32();
						linkMetadata.OriginatingUpdateSequenceNumber = binaryReader.ReadInt64();
						linkMetadata.LocalUpdateSequenceNumber = binaryReader.ReadInt64();
						memoryStream.Seek((long)num, SeekOrigin.Begin);
						linkMetadata.AttributeName = AttributeMetadata.ReadNullTerminatedString(binaryReader);
						memoryStream.Seek((long)num2, SeekOrigin.Begin);
						linkMetadata.TargetDistinguishedName = AttributeMetadata.ReadNullTerminatedString(binaryReader);
						memoryStream.Seek((long)num3, SeekOrigin.Begin);
						linkMetadata.Data = binaryReader.ReadBytes(count);
						return linkMetadata;
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
			throw new FormatException(DataStrings.InvalidFormat, innerException);
		}
	}
}
