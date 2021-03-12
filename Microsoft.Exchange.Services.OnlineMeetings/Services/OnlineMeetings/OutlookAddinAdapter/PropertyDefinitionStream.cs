using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000BF RID: 191
	internal class PropertyDefinitionStream
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x0000B480 File Offset: 0x00009680
		internal PropertyDefinitionStream(byte[] propDefBlob)
		{
			if (propDefBlob != null)
			{
				using (MemoryStream memoryStream = new MemoryStream(propDefBlob))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						this.Read(binaryReader);
					}
				}
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000B4EC File Offset: 0x000096EC
		internal List<FieldDefinitionStream> FieldDefinitions
		{
			get
			{
				return this.fieldDefinitions;
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000B4F4 File Offset: 0x000096F4
		internal void AddFieldDefinition(FieldDefinitionStream data)
		{
			this.fieldDefinitions.Add(data);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000B504 File Offset: 0x00009704
		internal byte[] GetByteArray()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					this.Write(binaryWriter);
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000B560 File Offset: 0x00009760
		private void Read(BinaryReader reader)
		{
			PropertyDefinitionStreamVersion version = (PropertyDefinitionStreamVersion)reader.ReadUInt16();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				FieldDefinitionStream item = FieldDefinitionStream.Read(reader, version);
				this.fieldDefinitions.Add(item);
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000B59C File Offset: 0x0000979C
		private void Write(BinaryWriter writer)
		{
			writer.Write(259);
			writer.Write(this.fieldDefinitions.Count);
			foreach (FieldDefinitionStream fieldDefinitionStream in this.fieldDefinitions)
			{
				fieldDefinitionStream.Write(writer);
			}
		}

		// Token: 0x04000314 RID: 788
		private readonly List<FieldDefinitionStream> fieldDefinitions = new List<FieldDefinitionStream>();
	}
}
