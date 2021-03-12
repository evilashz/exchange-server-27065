using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200034D RID: 845
	public abstract class PolicyContextMenuBase : ContextMenu
	{
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001FD4 RID: 8148 RVA: 0x000B83F1 File Offset: 0x000B65F1
		// (set) Token: 0x06001FD5 RID: 8149 RVA: 0x000B83F9 File Offset: 0x000B65F9
		private protected bool InheritChecked { protected get; private set; }

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x000B8402 File Offset: 0x000B6602
		// (set) Token: 0x06001FD7 RID: 8151 RVA: 0x000B840A File Offset: 0x000B660A
		private protected Guid PolicyChecked { protected get; private set; }

		// Token: 0x06001FD8 RID: 8152 RVA: 0x000B8413 File Offset: 0x000B6613
		protected PolicyContextMenuBase(string menuId, UserContext userContext, string clientClassName) : base(menuId, userContext)
		{
			if (string.IsNullOrEmpty(clientClassName))
			{
				throw new ArgumentException("clientClassName");
			}
			this.clientScript = "Owa.UIControls." + clientClassName + ".get_Instance(arguments[0]);";
			this.shouldScroll = true;
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x000B844D File Offset: 0x000B664D
		internal static string GetDefaultDisplayName(PolicyTag policyTag)
		{
			return string.Format(LocalizedStrings.GetNonEncoded(-1468060031), policyTag.Name, PolicyContextMenuBase.TimeSpanToString(policyTag.TimeSpanForRetention));
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x000B8470 File Offset: 0x000B6670
		internal static string TimeSpanToString(EnhancedTimeSpan timeSpan)
		{
			if (timeSpan == EnhancedTimeSpan.Zero)
			{
				return LocalizedStrings.GetNonEncoded(1491852291);
			}
			int num = (int)(timeSpan.TotalDays / 365.0);
			if (num > 0)
			{
				return string.Format(LocalizedStrings.GetNonEncoded((num > 1) ? 376120709 : 1543927794), num);
			}
			int num2 = (int)(timeSpan.TotalDays % 365.0 / 30.0);
			if (num2 > 0)
			{
				return string.Format(LocalizedStrings.GetNonEncoded((num2 > 1) ? 1489284924 : -228948015), num2);
			}
			int num3 = (int)timeSpan.TotalDays;
			return string.Format(LocalizedStrings.GetNonEncoded((num3 > 1) ? -620305904 : -912690831), num3);
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x000B8538 File Offset: 0x000B6738
		internal void SetStates(bool isInherited, Guid? tagGuid)
		{
			if (isInherited || tagGuid == null)
			{
				this.InheritChecked = true;
				this.PolicyChecked = Guid.Empty;
				return;
			}
			this.InheritChecked = false;
			this.PolicyChecked = tagGuid.Value;
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x000B8570 File Offset: 0x000B6770
		protected void RenderPolicyTagMenuItem(TextWriter output, Guid policyGuid, string policyDisplayName, bool disableChecked)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (policyGuid == Guid.Empty)
			{
				throw new ArgumentException("policyGuid");
			}
			if (string.IsNullOrEmpty(policyDisplayName))
			{
				throw new ArgumentException("policyDisplayName");
			}
			base.RenderMenuItem(output, policyDisplayName, object.Equals(policyGuid, this.PolicyChecked) ? ThemeFileId.MeetingAccept : ThemeFileId.Clear, policyGuid.ToString(), policyGuid.ToString(), disableChecked, null, null);
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x000B8600 File Offset: 0x000B6800
		protected void RenderInheritPolicyMenuItem(TextWriter output, bool withDivider, bool disableChecked)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (withDivider)
			{
				ContextMenu.RenderMenuDivider(output, "divS1");
			}
			base.RenderMenuItem(output, 1562439829, this.InheritChecked ? ThemeFileId.MeetingAccept : ThemeFileId.Clear, "inheritPolicy", "inheritPolicy", disableChecked);
		}

		// Token: 0x0400172B RID: 5931
		private const string InheritPolicy = "inheritPolicy";

		// Token: 0x0400172C RID: 5932
		private const ThemeFileId Checked = ThemeFileId.MeetingAccept;

		// Token: 0x0400172D RID: 5933
		private const ThemeFileId Unchecked = ThemeFileId.Clear;
	}
}
