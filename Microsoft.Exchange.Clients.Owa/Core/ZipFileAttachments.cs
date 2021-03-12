using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000297 RID: 663
	internal sealed class ZipFileAttachments
	{
		// Token: 0x06001976 RID: 6518 RVA: 0x00094AEA File Offset: 0x00092CEA
		private bool FileNameExists(string fileName)
		{
			return this.files.ContainsKey(fileName);
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00094AFD File Offset: 0x00092CFD
		internal ZipFileAttachments(BlockStatus blockStatus, string zipFileName)
		{
			this.files = new SortedDictionary<string, ZipEntryAttachment>(StringComparer.OrdinalIgnoreCase);
			this.blockStatus = blockStatus;
			this.zipFileName = this.ConditionZipFileNameForMimeHeader(zipFileName);
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00094B2C File Offset: 0x00092D2C
		public void AddAttachmentToZip(AttachmentWellInfo attachmentWellInfo, UserContext userContext, HttpContext httpContext)
		{
			if (attachmentWellInfo == null)
			{
				throw new ArgumentNullException("attachmentWellInfo");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (attachmentWellInfo.AttachmentType == AttachmentType.EmbeddedMessage)
			{
				this.hasEmbeddedItem = true;
			}
			bool doNeedToFilterHtml = AttachmentUtility.DoNeedToFilterHtml(attachmentWellInfo.MimeType, attachmentWellInfo.FileExtension, attachmentWellInfo.AttachmentLevel, userContext);
			bool isNotHtmlandNotXml = !AttachmentUtility.GetIsHtmlOrXml(attachmentWellInfo.MimeType, attachmentWellInfo.FileExtension);
			bool doNotSniff = AttachmentUtility.GetDoNotSniff(attachmentWellInfo.AttachmentLevel, userContext);
			string text = attachmentWellInfo.AttachmentName;
			if (attachmentWellInfo.AttachmentType == AttachmentType.EmbeddedMessage)
			{
				text += attachmentWellInfo.FileExtension;
			}
			string text2 = this.ConditionZipEntryFileName(text);
			attachmentWellInfo.FileName = text2;
			ZipEntryAttachment value = new ZipEntryAttachment(text2, attachmentWellInfo, doNeedToFilterHtml, doNotSniff, isNotHtmlandNotXml);
			this.files.Add(text2, value);
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00094BF8 File Offset: 0x00092DF8
		public void WriteArchive(HttpContext httpContext)
		{
			AttachmentUtility.SetResponseHeadersForZipAttachments(httpContext, this.zipFileName);
			List<string> list = new List<string>();
			if (this.hasEmbeddedItem)
			{
				foreach (KeyValuePair<string, ZipEntryAttachment> keyValuePair in this.files)
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (ZipEntryAttachment zipEntryAttachment in this.files.Values)
			{
				bool flag = zipEntryAttachment.AttachmentWellInfo.AttachmentType == AttachmentType.EmbeddedMessage;
				this.bytesWritten += zipEntryAttachment.WriteToStream(httpContext.Response.OutputStream, httpContext, this.blockStatus, this.bytesWritten, flag ? list : null);
			}
			long startOfDirectory = this.bytesWritten;
			foreach (ZipEntryAttachment zipEntryAttachment2 in this.files.Values)
			{
				this.bytesWritten += (long)((ulong)zipEntryAttachment2.WriteCentralDirectoryStructure(httpContext.Response.OutputStream));
			}
			long endOfDirectory = this.bytesWritten;
			this.WriteTrailer(httpContext.Response.OutputStream, startOfDirectory, endOfDirectory);
			httpContext.ApplicationInstance.CompleteRequest();
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00094D80 File Offset: 0x00092F80
		private void WriteTrailer(Stream outputStream, long startOfDirectory, long endOfDirectory)
		{
			ByteBuffer byteBuffer = new ByteBuffer(22);
			byteBuffer.WriteUInt32(101010256U);
			byteBuffer.WriteUInt16(0);
			byteBuffer.WriteUInt16(0);
			byteBuffer.WriteUInt16((ushort)this.files.Count);
			byteBuffer.WriteUInt16((ushort)this.files.Count);
			byteBuffer.WriteUInt32((uint)(endOfDirectory - startOfDirectory));
			byteBuffer.WriteUInt32((uint)startOfDirectory);
			byteBuffer.WriteUInt16(0);
			byteBuffer.WriteContentsTo(outputStream);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00094DFC File Offset: 0x00092FFC
		private string ConditionZipFileNameForMimeHeader(string fileName)
		{
			string text = fileName;
			if (fileName.Length > 148)
			{
				text = fileName.Substring(0, 148);
			}
			text = text.Replace(' ', '_');
			if (ZipFileAttachments.regexWindowsReservedNames.IsMatch(text))
			{
				text = text.Substring(0, text.Length - 1) + '_';
			}
			return text + ".zip";
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00094E64 File Offset: 0x00093064
		private string ConditionZipEntryFileName(string fileName)
		{
			string text = this.ToSafeFileNameString(fileName);
			string text2 = Path.GetFileNameWithoutExtension(text);
			bool flag = false;
			if (text.Length > 148)
			{
				int length = 148 - (text.Length - text2.Length);
				text2 = text2.Substring(0, length);
				flag = true;
			}
			int num = text2.Length - 1;
			if (text2[0] == '.')
			{
				text2 = '_' + text2.Substring(1, num);
				flag = true;
			}
			if (ZipFileAttachments.regexWindowsReservedNames.IsMatch(text2) || text2[num] == '.')
			{
				text2 = text2.Substring(0, num) + '_';
				flag = true;
			}
			string extension = Path.GetExtension(text);
			string text3 = string.Empty;
			if (!flag)
			{
				text3 = text;
			}
			else
			{
				text3 = text2 + extension;
			}
			int num2 = 1;
			while (this.FileNameExists(text3))
			{
				text3 = string.Format("{0}[{1}]{2}", text2, num2, extension);
				num2++;
			}
			return text3;
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00094F5C File Offset: 0x0009315C
		private string ToSafeFileNameString(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				throw new OwaInvalidInputException("Argument fileName may not be null or empty string");
			}
			StringBuilder stringBuilder = new StringBuilder(fileName.Length);
			for (int i = 0; i < fileName.Length; i++)
			{
				if (!this.IsFilenameOrPathInvalidChar(fileName[i]))
				{
					stringBuilder.Append(fileName[i]);
				}
				else
				{
					stringBuilder.Append('_');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00094FC8 File Offset: 0x000931C8
		private bool IsFilenameOrPathInvalidChar(char c)
		{
			if (char.IsControl(c))
			{
				return true;
			}
			if (c <= '/')
			{
				if (c != '"' && c != '*' && c != '/')
				{
					return false;
				}
			}
			else
			{
				switch (c)
				{
				case ':':
				case '<':
				case '>':
				case '?':
					break;
				case ';':
				case '=':
					return false;
				default:
					if (c != '\\' && c != '|')
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x04001298 RID: 4760
		private const int MaxFileNameLength = 148;

		// Token: 0x04001299 RID: 4761
		private const int MaxZipFileNameLength = 148;

		// Token: 0x0400129A RID: 4762
		private const string WindowsReservedNames = "(?i)^(com\\d$|lpt\\d$|con$|nul$|prn$|aux$)";

		// Token: 0x0400129B RID: 4763
		private const char Underscore = '_';

		// Token: 0x0400129C RID: 4764
		private static Regex regexWindowsReservedNames = new Regex("(?i)^(com\\d$|lpt\\d$|con$|nul$|prn$|aux$)");

		// Token: 0x0400129D RID: 4765
		private SortedDictionary<string, ZipEntryAttachment> files;

		// Token: 0x0400129E RID: 4766
		private BlockStatus blockStatus;

		// Token: 0x0400129F RID: 4767
		private string zipFileName;

		// Token: 0x040012A0 RID: 4768
		private long bytesWritten;

		// Token: 0x040012A1 RID: 4769
		private bool hasEmbeddedItem;
	}
}
