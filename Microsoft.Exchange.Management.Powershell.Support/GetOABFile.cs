using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.OAB;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000021 RID: 33
	[Cmdlet("Get", "OABFile")]
	public sealed class GetOABFile : Task
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000992C File Offset: 0x00007B2C
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00009934 File Offset: 0x00007B34
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public string Path { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000993D File Offset: 0x00007B3D
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00009945 File Offset: 0x00007B45
		[Parameter(Mandatory = false)]
		public SwitchParameter Data { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000994E File Offset: 0x00007B4E
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00009956 File Offset: 0x00007B56
		[Parameter(Mandatory = false)]
		public SwitchParameter Metadata { get; set; }

		// Token: 0x060001E4 RID: 484 RVA: 0x00009980 File Offset: 0x00007B80
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			using (FileStream fileStream = new FileStream(this.Path, FileMode.Open, FileAccess.Read))
			{
				using (BinaryReader reader = this.GetReader(fileStream))
				{
					OABFileHeader oabfileHeader = OABFileHeader.ReadFrom(reader);
					OABFileProperties oabfileProperties = OABFileProperties.ReadFrom(reader, "Properties");
					PropTag[] properties = Array.ConvertAll<OABPropertyDescriptor, PropTag>(oabfileProperties.HeaderProperties, (OABPropertyDescriptor propertyDescriptor) => propertyDescriptor.PropTag);
					OABFileRecord record = OABFileRecord.ReadFrom(reader, properties, "AddressListRecord");
					PropTag[] properties2 = Array.ConvertAll<OABPropertyDescriptor, PropTag>(oabfileProperties.DetailProperties, (OABPropertyDescriptor propertyDescriptor) => propertyDescriptor.PropTag);
					if (this.Metadata)
					{
						GetOABFile.FileMetadata fileMetadata = new GetOABFile.FileMetadata();
						fileMetadata.Version = oabfileHeader.Version;
						fileMetadata.RecordCount = oabfileHeader.RecordCount;
						fileMetadata.CRC = oabfileHeader.CRC;
						fileMetadata.AddressListProperties = Array.ConvertAll<OABPropertyDescriptor, GetOABFile.PropertyDescriptor>(oabfileProperties.HeaderProperties, (OABPropertyDescriptor propertyDescriptor) => new GetOABFile.PropertyDescriptor(propertyDescriptor));
						fileMetadata.RecordProperties = Array.ConvertAll<OABPropertyDescriptor, GetOABFile.PropertyDescriptor>(oabfileProperties.DetailProperties, (OABPropertyDescriptor propertyDescriptor) => new GetOABFile.PropertyDescriptor(propertyDescriptor));
						fileMetadata.AddressList = new GetOABFile.Record(record);
						GetOABFile.FileMetadata sendToPipeline = fileMetadata;
						base.WriteObject(sendToPipeline);
					}
					if (this.Data)
					{
						for (int i = 0; i < oabfileHeader.RecordCount; i++)
						{
							OABFileRecord record2 = OABFileRecord.ReadFrom(reader, properties2, "Record[" + i + "]");
							base.WriteObject(new GetOABFile.Record(record2));
						}
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00009B74 File Offset: 0x00007D74
		private BinaryReader GetReader(Stream stream)
		{
			string a;
			if ((a = System.IO.Path.GetExtension(this.Path).ToLower()) != null)
			{
				if (a == ".lzx")
				{
					return new BinaryReader(new OABDecompressStream(stream));
				}
				if (a == ".flt")
				{
					return new BinaryReader(stream);
				}
			}
			throw new InvalidDataException();
		}

		// Token: 0x02000022 RID: 34
		public sealed class FileMetadata
		{
			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x060001EB RID: 491 RVA: 0x00009BD1 File Offset: 0x00007DD1
			// (set) Token: 0x060001EC RID: 492 RVA: 0x00009BD9 File Offset: 0x00007DD9
			public int Version { get; set; }

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x060001ED RID: 493 RVA: 0x00009BE2 File Offset: 0x00007DE2
			// (set) Token: 0x060001EE RID: 494 RVA: 0x00009BEA File Offset: 0x00007DEA
			public int RecordCount { get; set; }

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060001EF RID: 495 RVA: 0x00009BF3 File Offset: 0x00007DF3
			// (set) Token: 0x060001F0 RID: 496 RVA: 0x00009BFB File Offset: 0x00007DFB
			public uint CRC { get; set; }

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x060001F1 RID: 497 RVA: 0x00009C04 File Offset: 0x00007E04
			// (set) Token: 0x060001F2 RID: 498 RVA: 0x00009C0C File Offset: 0x00007E0C
			public GetOABFile.PropertyDescriptor[] AddressListProperties { get; set; }

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x060001F3 RID: 499 RVA: 0x00009C15 File Offset: 0x00007E15
			// (set) Token: 0x060001F4 RID: 500 RVA: 0x00009C1D File Offset: 0x00007E1D
			public GetOABFile.PropertyDescriptor[] RecordProperties { get; set; }

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x060001F5 RID: 501 RVA: 0x00009C26 File Offset: 0x00007E26
			// (set) Token: 0x060001F6 RID: 502 RVA: 0x00009C2E File Offset: 0x00007E2E
			public GetOABFile.Record AddressList { get; set; }
		}

		// Token: 0x02000023 RID: 35
		[Flags]
		public enum PropertyFlags
		{
			// Token: 0x040000B5 RID: 181
			None = 0,
			// Token: 0x040000B6 RID: 182
			ANR = 1,
			// Token: 0x040000B7 RID: 183
			RDN = 2,
			// Token: 0x040000B8 RID: 184
			Index = 4,
			// Token: 0x040000B9 RID: 185
			Truncated = 8
		}

		// Token: 0x02000024 RID: 36
		public sealed class PropertyDescriptor
		{
			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x060001F8 RID: 504 RVA: 0x00009C3F File Offset: 0x00007E3F
			// (set) Token: 0x060001F9 RID: 505 RVA: 0x00009C47 File Offset: 0x00007E47
			public GetOABFile.PropertyIdentity Id { get; private set; }

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x060001FA RID: 506 RVA: 0x00009C50 File Offset: 0x00007E50
			// (set) Token: 0x060001FB RID: 507 RVA: 0x00009C58 File Offset: 0x00007E58
			public GetOABFile.PropertyFlags Flags { get; private set; }

			// Token: 0x060001FC RID: 508 RVA: 0x00009C61 File Offset: 0x00007E61
			internal PropertyDescriptor(OABPropertyDescriptor propertyDescriptor)
			{
				this.Id = new GetOABFile.PropertyIdentity(propertyDescriptor.PropTag);
				this.Flags = (GetOABFile.PropertyFlags)propertyDescriptor.PropFlags;
			}

			// Token: 0x060001FD RID: 509 RVA: 0x00009C88 File Offset: 0x00007E88
			public override string ToString()
			{
				return string.Concat(new object[]
				{
					"Id=",
					this.Id,
					",Flags=",
					this.Flags
				});
			}
		}

		// Token: 0x02000025 RID: 37
		public sealed class Record
		{
			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x060001FE RID: 510 RVA: 0x00009CC9 File Offset: 0x00007EC9
			// (set) Token: 0x060001FF RID: 511 RVA: 0x00009CD1 File Offset: 0x00007ED1
			public string Identity { get; private set; }

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x06000200 RID: 512 RVA: 0x00009CDA File Offset: 0x00007EDA
			// (set) Token: 0x06000201 RID: 513 RVA: 0x00009CE2 File Offset: 0x00007EE2
			public string DisplayName { get; private set; }

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x06000202 RID: 514 RVA: 0x00009CEB File Offset: 0x00007EEB
			// (set) Token: 0x06000203 RID: 515 RVA: 0x00009CF3 File Offset: 0x00007EF3
			public GetOABFile.PropertyValueCollection Properties { get; set; }

			// Token: 0x06000204 RID: 516 RVA: 0x00009CFC File Offset: 0x00007EFC
			internal Record(OABFileRecord record)
			{
				foreach (OABPropertyValue oabpropertyValue in record.PropertyValues)
				{
					if (oabpropertyValue != null)
					{
						if (oabpropertyValue.PropTag == PropTag.DisplayName)
						{
							this.DisplayName = (string)oabpropertyValue.Value;
						}
						if (oabpropertyValue.PropTag == (PropTag)2355953922U)
						{
							this.Identity = new Guid((byte[])oabpropertyValue.Value).ToString();
						}
					}
				}
				this.Properties = new GetOABFile.PropertyValueCollection(record.PropertyValues);
			}

			// Token: 0x06000205 RID: 517 RVA: 0x00009D8C File Offset: 0x00007F8C
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(1000);
				foreach (GetOABFile.PropertyIdentityValue propertyIdentityValue in this.Properties)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(propertyIdentityValue.ToString());
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x02000026 RID: 38
		public sealed class PropertyValueCollection : List<GetOABFile.PropertyIdentityValue>
		{
			// Token: 0x06000206 RID: 518 RVA: 0x00009E0C File Offset: 0x0000800C
			internal PropertyValueCollection(OABPropertyValue[] properties)
			{
				foreach (OABPropertyValue oabpropertyValue in properties)
				{
					if (oabpropertyValue != null)
					{
						base.Add(new GetOABFile.PropertyIdentityValue(oabpropertyValue));
					}
				}
			}

			// Token: 0x06000207 RID: 519 RVA: 0x00009E44 File Offset: 0x00008044
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(1000);
				foreach (GetOABFile.PropertyIdentityValue propertyIdentityValue in this)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(propertyIdentityValue.ToString());
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x02000027 RID: 39
		public sealed class PropertyIdentityValue
		{
			// Token: 0x170000BC RID: 188
			// (get) Token: 0x06000208 RID: 520 RVA: 0x00009EC0 File Offset: 0x000080C0
			// (set) Token: 0x06000209 RID: 521 RVA: 0x00009EC8 File Offset: 0x000080C8
			public GetOABFile.PropertyIdentity Id { get; private set; }

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x0600020A RID: 522 RVA: 0x00009ED1 File Offset: 0x000080D1
			// (set) Token: 0x0600020B RID: 523 RVA: 0x00009ED9 File Offset: 0x000080D9
			public GetOABFile.PropertyValue Value { get; private set; }

			// Token: 0x0600020C RID: 524 RVA: 0x00009EE2 File Offset: 0x000080E2
			internal PropertyIdentityValue(OABPropertyValue propertyValue)
			{
				this.Id = new GetOABFile.PropertyIdentity(propertyValue.PropTag);
				this.Value = new GetOABFile.PropertyValue(propertyValue.PropTag.ValueType(), propertyValue.Value);
			}

			// Token: 0x0600020D RID: 525 RVA: 0x00009F17 File Offset: 0x00008117
			public override string ToString()
			{
				return this.Id + "=" + this.Value;
			}
		}

		// Token: 0x02000028 RID: 40
		public sealed class PropertyIdentity
		{
			// Token: 0x170000BE RID: 190
			// (get) Token: 0x0600020E RID: 526 RVA: 0x00009F2F File Offset: 0x0000812F
			// (set) Token: 0x0600020F RID: 527 RVA: 0x00009F37 File Offset: 0x00008137
			public uint Id { get; private set; }

			// Token: 0x06000210 RID: 528 RVA: 0x00009F40 File Offset: 0x00008140
			internal PropertyIdentity(PropTag propTag)
			{
				this.Id = (uint)propTag;
			}

			// Token: 0x06000211 RID: 529 RVA: 0x00009F50 File Offset: 0x00008150
			public override string ToString()
			{
				if (Enum.IsDefined(typeof(PropTag), this.Id))
				{
					PropTag id = (PropTag)this.Id;
					return id.ToString();
				}
				if (Enum.IsDefined(typeof(CustomPropTag), this.Id))
				{
					CustomPropTag id2 = (CustomPropTag)this.Id;
					return id2.ToString();
				}
				return this.Id.ToString("X8");
			}
		}

		// Token: 0x02000029 RID: 41
		public sealed class PropertyValue
		{
			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000212 RID: 530 RVA: 0x00009FCE File Offset: 0x000081CE
			// (set) Token: 0x06000213 RID: 531 RVA: 0x00009FD6 File Offset: 0x000081D6
			public object Value { get; private set; }

			// Token: 0x06000214 RID: 532 RVA: 0x00009FDF File Offset: 0x000081DF
			internal PropertyValue(PropType propType, object value)
			{
				this.propType = propType;
				this.Value = value;
			}

			// Token: 0x06000215 RID: 533 RVA: 0x00009FF8 File Offset: 0x000081F8
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				PropTypeHandler handler = PropTypeHandler.GetHandler(this.propType);
				handler.AppendText(stringBuilder, this.Value);
				return stringBuilder.ToString();
			}

			// Token: 0x040000C2 RID: 194
			private PropType propType;
		}
	}
}
