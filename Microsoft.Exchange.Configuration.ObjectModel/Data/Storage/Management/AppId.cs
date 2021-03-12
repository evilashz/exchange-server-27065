using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000153 RID: 339
	[Serializable]
	public class AppId : XsoMailboxObjectId
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00026865 File Offset: 0x00024A65
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x0002685C File Offset: 0x00024A5C
		internal string AppIdValue { get; private set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0002686D File Offset: 0x00024A6D
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x00026875 File Offset: 0x00024A75
		internal string DisplayName { get; private set; }

		// Token: 0x06000C39 RID: 3129 RVA: 0x00026880 File Offset: 0x00024A80
		internal AppId(ADObjectId mailboxOwnerId, string displayName, string extensionId) : base(mailboxOwnerId ?? new ADObjectId())
		{
			if (string.IsNullOrEmpty(extensionId))
			{
				throw new ArgumentException(Strings.ErrorEmptyParameter(typeof(string).ToString()), "extensionId");
			}
			if (!GuidHelper.TryParseGuid(extensionId, out this.extensionGuid))
			{
				throw new ArgumentException(Strings.InvalidGuidParameter(extensionId), "extensionId");
			}
			this.AppIdValue = extensionId;
			this.DisplayName = displayName;
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x000268FC File Offset: 0x00024AFC
		public override byte[] GetBytes()
		{
			byte[] array = new byte[16];
			ExBitConverter.Write(this.extensionGuid, array, 0);
			byte[] bytes = base.MailboxOwnerId.GetBytes();
			byte[] array2 = new byte[array.Length + bytes.Length + 2];
			ExBitConverter.Write((short)bytes.Length, array2, 0);
			int num = 2;
			Array.Copy(bytes, 0, array2, num, bytes.Length);
			num += bytes.Length;
			Array.Copy(array, 0, array2, num, array.Length);
			return array2;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00026967 File Offset: 0x00024B67
		public override int GetHashCode()
		{
			return base.MailboxOwnerId.GetHashCode() ^ this.AppIdValue.GetHashCode();
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00026980 File Offset: 0x00024B80
		public override bool Equals(XsoMailboxObjectId other)
		{
			AppId appId = other as AppId;
			return !(null == appId) && ADObjectId.Equals(base.MailboxOwnerId, other.MailboxOwnerId) && string.Equals(this.AppIdValue, appId.AppIdValue);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x000269C5 File Offset: 0x00024BC5
		public override string ToString()
		{
			return string.Format("{0}{1}{2}", base.MailboxOwnerId, '\\', this.extensionGuid.ToString());
		}

		// Token: 0x040002CB RID: 715
		private const int BytesForGuid = 16;

		// Token: 0x040002CC RID: 716
		public const char MailboxAndExtensionSeparator = '\\';

		// Token: 0x040002CD RID: 717
		public const string ExtensionNameEscapedSeparator = "\\\\";

		// Token: 0x040002CE RID: 718
		private Guid extensionGuid;
	}
}
