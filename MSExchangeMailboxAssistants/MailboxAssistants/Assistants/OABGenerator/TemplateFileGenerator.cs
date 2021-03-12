using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.OAB;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001E8 RID: 488
	internal class TemplateFileGenerator
	{
		// Token: 0x0600131E RID: 4894 RVA: 0x0006E8A9 File Offset: 0x0006CAA9
		public TemplateFileGenerator(IConfigurationSession adSystemConfigSession, ADRawEntry addressTemplateContainer, OABDataFileType oabDataFileType, GenerationStats stats)
		{
			this.adSystemConfigSession = adSystemConfigSession;
			this.addressTemplateContainer = addressTemplateContainer;
			this.oabDataFileType = oabDataFileType;
			this.stats = stats;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0006E8E8 File Offset: 0x0006CAE8
		public OABFile GenerateTemplateFile(FileSet fileSet)
		{
			int templateEncoding = 0;
			if (!this.TryGetLcidFromContainerName(out templateEncoding))
			{
				return null;
			}
			FileStream fileStream = fileSet.Create("TPL");
			this.oabFile = new OABFile(fileStream, this.oabDataFileType);
			using (IOCostStream iocostStream = new IOCostStream(new NoCloseStream(fileStream)))
			{
				using (new FileSystemPerformanceTracker("GenerateOrLinkTemplateFiles.GenerateTemplateFiles", iocostStream, this.stats))
				{
					this.SetTemplateEncoding(templateEncoding);
					this.WriteHeader(iocostStream);
					this.GenerateAddressTemplates(iocostStream);
					this.GenerateDisplayTemplates(iocostStream);
				}
			}
			this.oabFile.Compress(fileSet, this.stats, "GenerateOrLinkTemplateFiles.GenerateTemplateFiles");
			return this.oabFile;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0006E9AC File Offset: 0x0006CBAC
		private bool TryGetLcidFromContainerName(out int lcid)
		{
			lcid = 0;
			Exception ex = null;
			try
			{
				lcid = int.Parse(this.addressTemplateContainer.Id.Name, NumberStyles.AllowHexSpecifier);
			}
			catch (ArgumentException ex2)
			{
				ex = ex2;
			}
			catch (FormatException ex3)
			{
				ex = ex3;
			}
			catch (OverflowException ex4)
			{
				ex = ex4;
			}
			if (ex == null)
			{
				return true;
			}
			OABLogger.LogRecord(TraceType.ErrorTrace, "TemplateFileGenerator.TryGetLcidFromContainerName: Could not parse a valid LCID from container name {0}.  Exception: {1}", new object[]
			{
				this.addressTemplateContainer.Id.Name,
				ex
			});
			return false;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0006EA48 File Offset: 0x0006CC48
		private void SetTemplateEncoding(int lcid)
		{
			this.oabFile.Lcid = lcid;
			if (lcid == 33809)
			{
				lcid = 1041;
			}
			try
			{
				CultureInfo cultureInfo = CultureInfo.GetCultureInfo(lcid);
				this.encoding = ((this.oabDataFileType == OABDataFileType.TemplateWin) ? Encoding.GetEncoding(cultureInfo.TextInfo.ANSICodePage) : Encoding.GetEncoding(cultureInfo.TextInfo.MacCodePage));
			}
			catch (ArgumentException)
			{
				CultureInfo cultureInfo = CultureInfo.GetCultureInfo(1033);
				this.encoding = ((this.oabDataFileType == OABDataFileType.TemplateWin) ? Encoding.GetEncoding(cultureInfo.TextInfo.ANSICodePage) : Encoding.GetEncoding(cultureInfo.TextInfo.MacCodePage));
			}
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0006EAFC File Offset: 0x0006CCFC
		private void GenerateAddressTemplates(Stream stream)
		{
			this.addressTemplates = this.adSystemConfigSession.Find<AddressTemplate>(this.addressTemplateContainer.Id, QueryScope.OneLevel, null, null, 0);
			this.addressTemplateDisplayTables = new byte[this.addressTemplates.Length][];
			for (int i = 0; i < this.addressTemplates.Length; i++)
			{
				byte[] displayTable = this.GetDisplayTable(this.addressTemplates[i]);
				this.addressTemplateDisplayTables[i] = displayTable;
			}
			this.WriteAddressTemplates(stream);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0006EB70 File Offset: 0x0006CD70
		private void GenerateDisplayTemplates(Stream stream)
		{
			ADObjectId childId = this.adSystemConfigSession.GetOrgContainerId().GetDescendantId(DetailsTemplate.ContainerId).GetChildId("CN", this.addressTemplateContainer.Id.Name);
			this.displayTemplates = this.adSystemConfigSession.Find<DetailsTemplate>(childId, QueryScope.OneLevel, null, null, 0);
			foreach (DetailsTemplate detailsTemplate in this.displayTemplates)
			{
				int num;
				if (int.TryParse(detailsTemplate.Name, out num) && num < 7)
				{
					byte[] displayTable = this.GetDisplayTable(detailsTemplate);
					if (displayTable != null)
					{
						this.displayTemplateDisplayTableOffsets[num] = (uint)this.oabFile.UncompressedFileStream.Position;
						this.displayTemplateDisplayTableSizes[num] = (uint)displayTable.Length;
						this.oabFile.UncompressedFileStream.Write(displayTable, 0, displayTable.Length);
					}
				}
			}
			this.WriteHeader(stream);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0006EC44 File Offset: 0x0006CE44
		private byte[] GetDisplayTable(object template)
		{
			byte[] array = (template is AddressTemplate) ? ((AddressTemplate)template).TemplateBlob : ((DetailsTemplate)template).TemplateBlob;
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(1U));
			int num = BitConverter.ToInt32(array, 4);
			list.AddRange(BitConverter.GetBytes(num));
			byte[] array2 = new byte[32];
			List<byte> list2 = new List<byte>();
			int num2 = 8 + num * 36;
			for (int i = 0; i < num; i++)
			{
				Array.Copy(array, 8 + i * 36, array2, 0, array2.Length);
				list.AddRange(array2);
				list.AddRange(BitConverter.GetBytes(num2 + list2.Count));
				int num3 = BitConverter.ToInt32(array, 8 + i * 36 + 32);
				int num4 = (i + 1 == num) ? array.Length : BitConverter.ToInt32(array, 8 + (i + 1) * 36 + 32);
				string @string = Encoding.Unicode.GetString(array, num3, num4 - num3);
				list2.AddRange(this.encoding.GetBytes(@string));
			}
			list.AddRange(list2);
			return list.ToArray();
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0006ED5C File Offset: 0x0006CF5C
		private void WriteHeader(Stream stream)
		{
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(7));
			list.AddRange(new byte[8]);
			for (int i = 0; i < 7; i++)
			{
				list.AddRange(new byte[8]);
				list.AddRange(BitConverter.GetBytes(this.displayTemplateDisplayTableOffsets[i]));
				list.AddRange(BitConverter.GetBytes(this.displayTemplateDisplayTableSizes[i]));
				list.AddRange(new byte[16]);
			}
			list.AddRange(new byte[4]);
			uint value = (uint)stream.Position;
			list.AddRange(BitConverter.GetBytes(value));
			list.AddRange(BitConverter.GetBytes(value));
			list.AddRange(BitConverter.GetBytes(value));
			list.AddRange((this.addressTemplates == null) ? new byte[4] : BitConverter.GetBytes(this.addressTemplates.Length));
			stream.Seek(0L, SeekOrigin.Begin);
			stream.Write(list.ToArray(), 0, list.Count);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0006EE4C File Offset: 0x0006D04C
		private void WriteAddressTemplates(Stream stream)
		{
			uint[] array = new uint[this.addressTemplates.Length];
			uint[] array2 = new uint[this.addressTemplates.Length];
			uint[] array3 = new uint[this.addressTemplates.Length];
			uint[] array4 = new uint[this.addressTemplates.Length];
			uint[] array5 = new uint[this.addressTemplates.Length];
			uint[] array6 = new uint[this.addressTemplates.Length];
			uint[] array7 = new uint[this.addressTemplates.Length];
			uint[] array8 = new uint[this.addressTemplates.Length];
			List<byte> list = new List<byte>();
			uint num = (uint)stream.Position + (uint)(this.addressTemplates.Length * 8 * 4);
			for (int i = 0; i < this.addressTemplates.Length; i++)
			{
				array[i] = num;
				list.AddRange(this.encoding.GetBytes(this.addressTemplates[i].ExchangeLegacyDN));
				list.Add(0);
				num += (array2[i] = (uint)(this.addressTemplates[i].ExchangeLegacyDN.Length + 1));
				array7[i] = num;
				byte[] bytes = this.encoding.GetBytes(this.addressTemplates[i].DisplayName);
				list.AddRange(bytes);
				list.Add(0);
				num += (array8[i] = (uint)(bytes.Length + 1));
			}
			for (int j = 0; j < this.addressTemplates.Length; j++)
			{
				array3[j] = num;
				list.AddRange(this.addressTemplateDisplayTables[j]);
				num += (array4[j] = (uint)this.addressTemplates[j].TemplateBlob.Length);
				array5[j] = num;
				list.AddRange(this.addressTemplates[j].AddressSyntax);
				num += (array6[j] = (uint)this.addressTemplates[j].AddressSyntax.Length);
			}
			List<byte> list2 = new List<byte>();
			for (int k = 0; k < this.addressTemplates.Length; k++)
			{
				list2.AddRange(BitConverter.GetBytes(array[k]));
				list2.AddRange(BitConverter.GetBytes(array2[k]));
				list2.AddRange(BitConverter.GetBytes(array3[k]));
				list2.AddRange(BitConverter.GetBytes(array4[k]));
				list2.AddRange(BitConverter.GetBytes(array5[k]));
				list2.AddRange(BitConverter.GetBytes(array6[k]));
				list2.AddRange(BitConverter.GetBytes(array7[k]));
				list2.AddRange(BitConverter.GetBytes(array8[k]));
			}
			stream.Write(list2.ToArray(), 0, list2.Count);
			stream.Write(list.ToArray(), 0, list.Count);
		}

		// Token: 0x04000B84 RID: 2948
		private ADRawEntry addressTemplateContainer;

		// Token: 0x04000B85 RID: 2949
		private OABDataFileType oabDataFileType;

		// Token: 0x04000B86 RID: 2950
		private AddressTemplate[] addressTemplates;

		// Token: 0x04000B87 RID: 2951
		private DetailsTemplate[] displayTemplates;

		// Token: 0x04000B88 RID: 2952
		private OABFile oabFile;

		// Token: 0x04000B89 RID: 2953
		private Encoding encoding;

		// Token: 0x04000B8A RID: 2954
		private byte[][] addressTemplateDisplayTables;

		// Token: 0x04000B8B RID: 2955
		private uint[] displayTemplateDisplayTableOffsets = new uint[7];

		// Token: 0x04000B8C RID: 2956
		private uint[] displayTemplateDisplayTableSizes = new uint[7];

		// Token: 0x04000B8D RID: 2957
		private IConfigurationSession adSystemConfigSession;

		// Token: 0x04000B8E RID: 2958
		private readonly GenerationStats stats;
	}
}
