using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000158 RID: 344
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OABManifestFile
	{
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00039812 File Offset: 0x00037A12
		// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x0003981A File Offset: 0x00037A1A
		public OABDataFileType Type { get; set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00039823 File Offset: 0x00037A23
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x0003982B File Offset: 0x00037A2B
		public uint Sequence { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x00039834 File Offset: 0x00037A34
		// (set) Token: 0x06000DBA RID: 3514 RVA: 0x0003983C File Offset: 0x00037A3C
		public string Version { get; set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00039845 File Offset: 0x00037A45
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x0003984D File Offset: 0x00037A4D
		public uint CompressedSize { get; set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x00039856 File Offset: 0x00037A56
		// (set) Token: 0x06000DBE RID: 3518 RVA: 0x0003985E File Offset: 0x00037A5E
		public uint UncompressedSize { get; set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x00039867 File Offset: 0x00037A67
		// (set) Token: 0x06000DC0 RID: 3520 RVA: 0x0003986F File Offset: 0x00037A6F
		public string Hash { get; set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00039878 File Offset: 0x00037A78
		// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x00039880 File Offset: 0x00037A80
		public int? Langid { get; set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00039889 File Offset: 0x00037A89
		// (set) Token: 0x06000DC4 RID: 3524 RVA: 0x00039891 File Offset: 0x00037A91
		public string FileName { get; set; }

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0003989C File Offset: 0x00037A9C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			stringBuilder.Append("Type=");
			stringBuilder.Append(this.Type.ToString());
			stringBuilder.Append(", Sequence:");
			stringBuilder.Append(this.Sequence.ToString());
			stringBuilder.Append(", Version:");
			stringBuilder.Append(this.Version);
			stringBuilder.Append(", CompressedSize:");
			stringBuilder.Append(this.CompressedSize.ToString());
			stringBuilder.Append(", UncompressedSize:");
			stringBuilder.Append(this.UncompressedSize.ToString());
			stringBuilder.Append(", Hash:");
			stringBuilder.Append(this.Hash);
			if (this.Langid != null)
			{
				stringBuilder.Append(", Langid:");
				stringBuilder.Append(this.Langid.Value.ToString());
			}
			stringBuilder.Append(", FileName:");
			stringBuilder.Append(this.FileName);
			return stringBuilder.ToString();
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x000399C8 File Offset: 0x00037BC8
		internal static OABManifestFile Deserialize(Stream stream, XmlReader reader)
		{
			long position = stream.Position;
			string name;
			string attribute;
			string attribute2;
			string attribute3;
			string attribute4;
			string attribute5;
			string attribute6;
			string attribute7;
			string text;
			try
			{
				name = reader.Name;
				attribute = reader.GetAttribute("seq");
				attribute2 = reader.GetAttribute("ver");
				attribute3 = reader.GetAttribute("size");
				attribute4 = reader.GetAttribute("uncompressedsize");
				attribute5 = reader.GetAttribute("SHA");
				attribute6 = reader.GetAttribute("langid");
				attribute7 = reader.GetAttribute("type");
				reader.ReadStartElement();
				text = reader.ReadString().Trim();
				reader.ReadEndElement();
			}
			catch (XmlException arg)
			{
				throw new InvalidDataException(string.Format("Invalid element at stream position {0} due exception: {1}", position, arg));
			}
			string a;
			if ((a = name) != null)
			{
				OABDataFileType type;
				if (!(a == "Full"))
				{
					if (!(a == "Diff"))
					{
						if (!(a == "Template"))
						{
							goto IL_11F;
						}
						string a2;
						if ((a2 = attribute7) != null)
						{
							if (a2 == "windows")
							{
								type = OABDataFileType.TemplateWin;
								goto IL_136;
							}
							if (a2 == "mac")
							{
								type = OABDataFileType.TemplateMac;
								goto IL_136;
							}
						}
						throw new InvalidDataException(string.Format("Invalid element at stream position {0} because 'type' attribute has unexpected value: {1}", position, attribute7));
					}
					else
					{
						type = OABDataFileType.Diff;
					}
				}
				else
				{
					type = OABDataFileType.Full;
				}
				IL_136:
				uint sequence;
				if (!uint.TryParse(attribute, out sequence))
				{
					throw new InvalidDataException(string.Format("Ignoring element at stream position {0} because 'seq' attribute has unexpected value: {1}", position, attribute));
				}
				if (string.IsNullOrWhiteSpace(attribute2))
				{
					throw new InvalidDataException(string.Format("Ignoring element at stream position {0} because 'ver' attribute has empty value: {1}", position, attribute2));
				}
				uint compressedSize;
				if (!uint.TryParse(attribute3, out compressedSize))
				{
					throw new InvalidDataException(string.Format("Ignoring element at stream position {0} because 'size' attribute has unexpected value: {1}", position, attribute3));
				}
				uint uncompressedSize;
				if (!uint.TryParse(attribute4, out uncompressedSize))
				{
					throw new InvalidDataException(string.Format("Ignoring element at stream position {0} 'uncompressedsize' attribute has unexpected value: {1}", position, attribute4));
				}
				int? langid = null;
				if (!string.IsNullOrWhiteSpace(attribute6))
				{
					int value;
					if (!int.TryParse(attribute6, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
					{
						throw new InvalidDataException(string.Format("Ignoring element at stream position {0} because 'langid' attribute has unexpected value: {1}", position, attribute6));
					}
					langid = new int?(value);
				}
				if (string.IsNullOrWhiteSpace(text))
				{
					throw new InvalidDataException(string.Format("Ignoring element at stream position {0} because element has empty value: {1}", position, text));
				}
				return new OABManifestFile
				{
					Type = type,
					Sequence = sequence,
					Version = attribute2,
					CompressedSize = compressedSize,
					UncompressedSize = uncompressedSize,
					Hash = attribute5,
					Langid = langid,
					FileName = text
				};
			}
			IL_11F:
			throw new InvalidDataException(string.Format("Ignoring element at stream position {0} because it is an unknown element: {1}", position, name));
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00039C5C File Offset: 0x00037E5C
		internal void Serialize(XmlWriter writer)
		{
			writer.WriteStartElement(OABManifestFile.GetFileType(this.Type));
			writer.WriteAttributeString("seq", this.Sequence.ToString());
			writer.WriteAttributeString("ver", this.Version);
			writer.WriteAttributeString("size", this.CompressedSize.ToString());
			writer.WriteAttributeString("uncompressedsize", this.UncompressedSize.ToString());
			writer.WriteAttributeString("SHA", this.Hash);
			if (this.Langid != null)
			{
				writer.WriteAttributeString("langid", this.Langid.Value.ToString("x4"));
			}
			string templateType = OABManifestFile.GetTemplateType(this.Type);
			if (templateType != null)
			{
				writer.WriteAttributeString("type", templateType);
			}
			writer.WriteString(this.FileName);
			writer.WriteEndElement();
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00039D50 File Offset: 0x00037F50
		private static string GetFileType(OABDataFileType fileType)
		{
			switch (fileType)
			{
			case OABDataFileType.Full:
				return "Full";
			case OABDataFileType.Diff:
				return "Diff";
			case OABDataFileType.TemplateMac:
			case OABDataFileType.TemplateWin:
				return "Template";
			default:
				throw new ArgumentException("fileType");
			}
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00039D94 File Offset: 0x00037F94
		private static string GetTemplateType(OABDataFileType fileType)
		{
			switch (fileType)
			{
			case OABDataFileType.Full:
			case OABDataFileType.Diff:
				return null;
			case OABDataFileType.TemplateMac:
				return "mac";
			case OABDataFileType.TemplateWin:
				return "windows";
			default:
				throw new ArgumentException("fileType");
			}
		}
	}
}
