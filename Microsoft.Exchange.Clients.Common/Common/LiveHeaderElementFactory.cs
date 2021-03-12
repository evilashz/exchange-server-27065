using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x0200001F RID: 31
	internal static class LiveHeaderElementFactory
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00007CA0 File Offset: 0x00005EA0
		internal static ILiveHeaderElement GetLiveHeaderElement(LiveAssetReader reader, string stringId)
		{
			if (stringId == "Live.Shared.GlobalSettings.Header.Tabs.Link.Separator")
			{
				return LiveHeaderElementFactory.CreateNewSeparator();
			}
			if (stringId == "Live.Shared.GlobalSettings.Header.Tabs.Link.Messenger")
			{
				return null;
			}
			string @string = reader.GetString(stringId);
			string[] array = LiveHeaderElementFactory.SplitString(@string);
			ILiveHeaderElement result;
			if (array.Length <= 1 && @string.Contains("Separator"))
			{
				result = LiveHeaderElementFactory.CreateNewSeparator();
			}
			else if (@string.Contains(".Link"))
			{
				result = LiveHeaderElementFactory.CreateNewList(reader, array);
			}
			else if (@string.Contains(".Menu") || @string.Contains(".Items") || @string.EndsWith(".Group", StringComparison.Ordinal))
			{
				result = LiveHeaderElementFactory.CreateNewMenu(reader, array);
			}
			else
			{
				result = LiveHeaderElementFactory.CreateNewLink(reader, array);
			}
			return result;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00007D4C File Offset: 0x00005F4C
		private static void AddLiveHeaderElement(LiveAssetReader reader, string stringId, IList<ILiveHeaderElement> parent)
		{
			ILiveHeaderElement liveHeaderElement = LiveHeaderElementFactory.GetLiveHeaderElement(reader, stringId);
			if (liveHeaderElement != null)
			{
				parent.Add(liveHeaderElement);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00007D6C File Offset: 0x00005F6C
		private static string[] SplitString(string wholeValue)
		{
			return wholeValue.Replace(" ", string.Empty).Split(new char[]
			{
				','
			});
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007DA0 File Offset: 0x00005FA0
		private static LiveHeaderMenuSeparator CreateNewSeparator()
		{
			return new LiveHeaderMenuSeparator();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007DB4 File Offset: 0x00005FB4
		private static LiveHeaderLink CreateNewLink(LiveAssetReader reader, string[] value)
		{
			LiveHeaderLink liveHeaderLink = new LiveHeaderLink();
			foreach (string text in value)
			{
				if (text.Contains(".Text") || text.Contains(".Name"))
				{
					liveHeaderLink.Text = reader.GetString(text);
				}
				else if (text.Contains(".Title"))
				{
					liveHeaderLink.Title = reader.GetString(text);
				}
				else if (text.Contains(".Href") || text.Contains(".Url"))
				{
					liveHeaderLink.Href = reader.GetString(text);
				}
			}
			if (string.IsNullOrEmpty(liveHeaderLink.Text))
			{
				return null;
			}
			return liveHeaderLink;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007E58 File Offset: 0x00006058
		private static LiveHeaderMenu CreateNewMenu(LiveAssetReader reader, string[] value)
		{
			LiveHeaderMenu liveHeaderMenu = new LiveHeaderMenu();
			liveHeaderMenu.Link = LiveHeaderElementFactory.CreateNewLink(reader, value);
			if (liveHeaderMenu.Link == null)
			{
				return null;
			}
			foreach (string text in value)
			{
				if (text.Contains("Menu") || text.Contains(".Items") || text.EndsWith("Group", StringComparison.Ordinal))
				{
					string @string = reader.GetString(text);
					if (!string.IsNullOrEmpty(@string))
					{
						liveHeaderMenu.List = LiveHeaderElementFactory.CreateNewList(reader, LiveHeaderElementFactory.SplitString(@string));
						if (LiveHeaderElementFactory.IsCobrandMenu(text) && reader.IsPropertySet(LiveAssetKey.OpenCustomLinksInNewWindow))
						{
							foreach (ILiveHeaderElement liveHeaderElement in liveHeaderMenu.List)
							{
								LiveHeaderLink liveHeaderLink = liveHeaderElement as LiveHeaderLink;
								if (liveHeaderLink != null)
								{
									liveHeaderLink.OpenInNewWindow = true;
								}
							}
						}
					}
				}
			}
			if (liveHeaderMenu.List == null || liveHeaderMenu.List.Count == 0)
			{
				return null;
			}
			return liveHeaderMenu;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007F6C File Offset: 0x0000616C
		private static LiveHeaderLinkCollection CreateNewList(LiveAssetReader reader, string[] value)
		{
			LiveHeaderLinkCollection liveHeaderLinkCollection = new LiveHeaderLinkCollection();
			foreach (string text in value)
			{
				if (text.Contains("Separator"))
				{
					if (liveHeaderLinkCollection.Count > 0)
					{
						ILiveHeaderElement liveHeaderElement = liveHeaderLinkCollection[liveHeaderLinkCollection.Count - 1];
						if (liveHeaderElement is LiveHeaderLink || liveHeaderElement is LiveHeaderMenu)
						{
							LiveHeaderElementFactory.AddLiveHeaderElement(reader, text, liveHeaderLinkCollection);
						}
					}
				}
				else
				{
					LiveHeaderElementFactory.AddLiveHeaderElement(reader, text, liveHeaderLinkCollection);
				}
			}
			return liveHeaderLinkCollection;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00007FE0 File Offset: 0x000061E0
		private static bool IsCobrandMenu(string item)
		{
			return LiveAssetKeys.GetAssetKeyString(LiveAssetKey.CobrandingCustomMenu).Equals(item);
		}
	}
}
