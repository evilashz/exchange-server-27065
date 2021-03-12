using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.UM.UMCommon.FaultInjection;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200006D RID: 109
	internal class DRMUtils
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		internal static string GetProtectedUMFileNameToUse(string attachmentFileName)
		{
			if (string.IsNullOrEmpty(attachmentFileName))
			{
				return attachmentFileName;
			}
			attachmentFileName = attachmentFileName.Trim();
			string extension;
			string str;
			if (!Attachment.TryFindFileExtension(attachmentFileName, out extension, out str))
			{
				return attachmentFileName;
			}
			string str2;
			if (AudioFile.TryGetDRMFileExtension(extension, out str2))
			{
				return str + str2;
			}
			return attachmentFileName;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000E200 File Offset: 0x0000C400
		internal static string GetProtectedUMVoiceMessageAttachmentOrder(string currentOrder)
		{
			if (currentOrder == null)
			{
				return null;
			}
			string result = currentOrder;
			string[] array = currentOrder.Split(new char[]
			{
				';'
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length > 0)
			{
				StringBuilder stringBuilder = new StringBuilder(DRMUtils.GetProtectedUMFileNameToUse(array[0]));
				for (int i = 1; i < array.Length; i++)
				{
					stringBuilder.Append(";").Append(DRMUtils.GetProtectedUMFileNameToUse(array[i]));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000E270 File Offset: 0x0000C470
		internal static MessageItem OpenRestrictedContent(MessageItem restrictedContentItem, OrganizationId orgId)
		{
			MessageItem result = null;
			try
			{
				RestrictionInfo restrictionInfo = null;
				FaultInjectionUtils.FaultInjectException(4108725565U);
				bool flag;
				UseLicenseAndUsageRights useLicenseAndUsageRights;
				result = ItemConversion.OpenRestrictedContent(restrictedContentItem, orgId, true, out flag, out useLicenseAndUsageRights, out restrictionInfo);
			}
			catch (CorruptDataException ex)
			{
				throw new OpenRestrictedContentException(ex.Message, ex);
			}
			catch (RightsManagementPermanentException ex2)
			{
				throw new OpenRestrictedContentException(ex2.Message, ex2);
			}
			catch (RightsManagementTransientException ex3)
			{
				throw new OpenRestrictedContentException(ex3.Message, ex3);
			}
			catch (InvalidOperationException ex4)
			{
				throw new OpenRestrictedContentException(ex4.Message, ex4);
			}
			catch (LocalizedException ex5)
			{
				throw new OpenRestrictedContentException(ex5.Message, ex5);
			}
			return result;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000E334 File Offset: 0x0000C534
		internal static Stream OpenProtectedAttachment(Attachment sourceAttachment, OrganizationId orgId)
		{
			Stream stream = null;
			try
			{
				FaultInjectionUtils.FaultInjectException(2900766013U);
				stream = StreamAttachment.OpenRestrictedContent(sourceAttachment as StreamAttachment, orgId);
			}
			catch (RightsManagementPermanentException ex)
			{
				throw new OpenRestrictedContentException(ex.Message, ex);
			}
			catch (RightsManagementTransientException ex2)
			{
				throw new OpenRestrictedContentException(ex2.Message, ex2);
			}
			if (stream == null)
			{
				throw new OpenRestrictedContentException("Attachment Stream is null");
			}
			return stream;
		}
	}
}
