using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C62 RID: 3170
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class FolderHomePageUrlProperty : SmartPropertyDefinition
	{
		// Token: 0x06006FA5 RID: 28581 RVA: 0x001E0CB4 File Offset: 0x001DEEB4
		internal FolderHomePageUrlProperty() : base("FolderHomePageUrlProperty", typeof(string), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.FolderWebViewInfo, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006FA6 RID: 28582 RVA: 0x001E0CF4 File Offset: 0x001DEEF4
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(InternalSchema.FolderWebViewInfo);
			if (PropertyError.IsPropertyNotFound(value))
			{
				return value;
			}
			byte[] webViewInfo = PropertyBag.CheckPropertyValue<byte[]>(InternalSchema.FolderWebViewInfo, value);
			object result;
			try
			{
				result = FolderHomePageUrlProperty.GetUrlFromWebViewInfo(webViewInfo);
			}
			catch (CorruptDataException ex)
			{
				result = new PropertyError(this, PropertyErrorCode.CorruptedData, ex.Message);
			}
			return result;
		}

		// Token: 0x06006FA7 RID: 28583 RVA: 0x001E0D50 File Offset: 0x001DEF50
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			string folderHomePageUrl = (string)value;
			byte[] propertyValue = FolderHomePageUrlProperty.CreateWebViewInformation(folderHomePageUrl);
			propertyBag.SetValueWithFixup(InternalSchema.FolderWebViewInfo, propertyValue);
		}

		// Token: 0x06006FA8 RID: 28584 RVA: 0x001E0D78 File Offset: 0x001DEF78
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(InternalSchema.FolderWebViewInfo);
		}

		// Token: 0x06006FA9 RID: 28585 RVA: 0x001E0D88 File Offset: 0x001DEF88
		private static byte[] CreateWebViewInformation(string folderHomePageUrl)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream, Encoding.Unicode))
				{
					binaryWriter.Write(2U);
					binaryWriter.Write(1U);
					binaryWriter.Write(1U);
					for (int i = 0; i < 7; i++)
					{
						binaryWriter.Write(0U);
					}
					char[] array = folderHomePageUrl.ToCharArray();
					binaryWriter.Write((uint)(array.Length * 2 + 2));
					binaryWriter.Write(array);
					binaryWriter.Write('\0');
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06006FAA RID: 28586 RVA: 0x001E0E30 File Offset: 0x001DF030
		private static string GetUrlFromWebViewInfo(byte[] webViewInfo)
		{
			if (webViewInfo.Length == 0)
			{
				return string.Empty;
			}
			string result;
			try
			{
				string @string;
				using (MemoryStream memoryStream = new MemoryStream(webViewInfo))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						binaryReader.BaseStream.Seek(40L, SeekOrigin.Begin);
						int num = (int)(binaryReader.ReadUInt32() - 2U);
						if (num == 0)
						{
							return string.Empty;
						}
						if (num < 14)
						{
							throw new CorruptDataException(ServerStrings.ExCorruptFolderWebViewInfo);
						}
						byte[] array = binaryReader.ReadBytes(num);
						if (array.Length != num)
						{
							throw new CorruptDataException(ServerStrings.ExCorruptFolderWebViewInfo);
						}
						@string = Encoding.Unicode.GetString(array, 0, array.Length);
					}
				}
				result = @string;
			}
			catch (EndOfStreamException innerException)
			{
				throw new CorruptDataException(ServerStrings.ExCorruptFolderWebViewInfo, innerException);
			}
			return result;
		}

		// Token: 0x040043AB RID: 17323
		private const uint WebViewVersion = 2U;

		// Token: 0x040043AC RID: 17324
		private const uint WebViewType = 1U;

		// Token: 0x040043AD RID: 17325
		private const uint WebViewFlagsShowByDefault = 1U;

		// Token: 0x040043AE RID: 17326
		private const uint WebViewUnused = 0U;

		// Token: 0x040043AF RID: 17327
		private const long WebViewUninterestingPortion = 40L;

		// Token: 0x040043B0 RID: 17328
		private const int MinURLLength = 7;
	}
}
