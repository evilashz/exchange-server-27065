using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000224 RID: 548
	internal class EventCategorySession : IConfigDataProvider
	{
		// Token: 0x0600132D RID: 4909 RVA: 0x0003A9DF File Offset: 0x00038BDF
		public EventCategorySession(string source)
		{
			this.source = source;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0003A9F0 File Offset: 0x00038BF0
		IConfigurable IConfigDataProvider.Read<T>(ObjectId identity)
		{
			EventCategoryIdentity eventCategoryIdentity = identity as EventCategoryIdentity;
			if (eventCategoryIdentity == null)
			{
				throw new ExArgumentNullException("categoryId");
			}
			return EventCategorySession.GetCategory(eventCategoryIdentity);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0003AA18 File Offset: 0x00038C18
		IConfigurable[] IConfigDataProvider.Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			EventCategoryIdentity categoryId = rootId as EventCategoryIdentity;
			return EventCategorySession.GetCategories(categoryId);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0003AA34 File Offset: 0x00038C34
		IEnumerable<T> IConfigDataProvider.FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			EventCategoryIdentity categoryId = rootId as EventCategoryIdentity;
			List<T> list = new List<T>();
			IConfigurable[] array = (IConfigurable[])EventCategorySession.GetCategories(categoryId);
			foreach (IConfigurable configurable in array)
			{
				list.Add((T)((object)configurable));
			}
			return list;
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x0003AA83 File Offset: 0x00038C83
		string IConfigDataProvider.Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0003AA8C File Offset: 0x00038C8C
		public void Save(IConfigurable instance)
		{
			EventCategoryObject eventCategoryObject = instance as EventCategoryObject;
			if (eventCategoryObject == null)
			{
				throw new ArgumentException();
			}
			EventCategorySession.SetCategory(eventCategoryObject);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0003AAAF File Offset: 0x00038CAF
		public void Delete(IConfigurable instance)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0003AAB8 File Offset: 0x00038CB8
		private static RegistryKey GetCategoryRegistryKey(EventCategoryIdentity id, bool fWriteAccess, out string keyName)
		{
			RegistryKey registryKey = null;
			keyName = null;
			if (id == null)
			{
				throw new ExArgumentNullException("id");
			}
			RegistryKey registryKey2;
			if (id.Server == null)
			{
				registryKey2 = Registry.LocalMachine;
			}
			else
			{
				registryKey2 = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, id.Server);
				registryKey = registryKey2;
			}
			registryKey2 = registryKey2.OpenSubKey(string.Format("System\\CurrentControlSet\\Services\\{0}\\Diagnostics\\", id.EventSource), fWriteAccess);
			if (registryKey != null)
			{
				registryKey.Close();
			}
			if (registryKey2 == null)
			{
				throw new DataSourceOperationException(DataStrings.ExceptionEventSourceNotFound(id.EventSource));
			}
			if (id.SubEventSource != null)
			{
				registryKey = registryKey2;
				registryKey2 = registryKey2.OpenSubKey(id.SubEventSource, fWriteAccess);
				if (registryKey != null)
				{
					registryKey.Close();
				}
				if (registryKey2 == null)
				{
					throw new DataSourceOperationException(DataStrings.ExceptionEventSourceNotFound(id.EventSource + "\\" + id.SubEventSource));
				}
			}
			if (id.Category != null)
			{
				foreach (string text in registryKey2.GetValueNames())
				{
					if (string.Compare(text.Substring(text.IndexOf(' ') + 1), id.Category, StringComparison.OrdinalIgnoreCase) == 0)
					{
						keyName = text;
					}
				}
			}
			return registryKey2;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0003ABC4 File Offset: 0x00038DC4
		private static EventCategoryObject[] GetCategories(EventCategoryIdentity categoryId)
		{
			List<EventCategoryObject> list = new List<EventCategoryObject>();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			if (categoryId != null)
			{
				if (categoryId.EventSource != null)
				{
					text2 = categoryId.EventSource;
					if (text2.StartsWith("*"))
					{
						flag3 = true;
						text2 = text2.Remove(0, 1);
					}
					if (text2.EndsWith("*"))
					{
						flag4 = true;
						text2 = text2.Remove(text2.Length - 1, 1);
					}
				}
				if (categoryId.SubEventSource != null)
				{
					text3 = categoryId.SubEventSource;
					if (text3.StartsWith("*"))
					{
						flag5 = true;
						text3 = text3.Remove(0, 1);
					}
					if (text3.EndsWith("*"))
					{
						flag6 = true;
						text3 = text3.Remove(text3.Length - 1, 1);
					}
				}
				if (categoryId.Category != null)
				{
					text = categoryId.Category;
					if (text.StartsWith("*"))
					{
						flag = true;
						text = text.Remove(0, 1);
					}
					if (text.EndsWith("*"))
					{
						flag2 = true;
						text = text.Remove(text.Length - 1, 1);
					}
				}
			}
			if (categoryId == null || categoryId.EventSource == null || flag3 || flag4 || flag5 || flag6)
			{
				foreach (string text5 in EventCategoryIdentity.EventSources)
				{
					string text6;
					if (categoryId != null && categoryId.Server != null)
					{
						text6 = categoryId.Server + "\\" + text5;
					}
					else
					{
						text6 = text5;
					}
					if ((!flag3 && !flag4) || (flag3 && text5.EndsWith(text2, StringComparison.OrdinalIgnoreCase)) || (flag4 && text5.StartsWith(text2, StringComparison.OrdinalIgnoreCase)) || (flag3 && flag4 && text5.ToLowerInvariant().Contains(text2.ToLowerInvariant())))
					{
						if (text != null && !flag2 && !flag)
						{
							text6 = text6 + "\\" + text;
						}
						try
						{
							EventCategoryObject[] categories = EventCategorySession.GetCategories(EventCategoryIdentity.Parse(text6));
							foreach (EventCategoryObject eventCategoryObject in categories)
							{
								EventCategoryIdentity eventCategoryIdentity = (EventCategoryIdentity)eventCategoryObject.Identity;
								if ((eventCategoryIdentity.SubEventSource != null || (!flag5 && !flag6)) && ((!flag5 && !flag6) || (flag5 && eventCategoryIdentity.SubEventSource.EndsWith(text3, StringComparison.OrdinalIgnoreCase)) || (flag6 && eventCategoryIdentity.SubEventSource.StartsWith(text3, StringComparison.OrdinalIgnoreCase)) || (flag5 && flag6 && eventCategoryIdentity.SubEventSource.ToLowerInvariant().Contains(text3.ToLowerInvariant()))) && ((!flag && !flag2) || (flag && eventCategoryIdentity.Category.EndsWith(text, StringComparison.OrdinalIgnoreCase)) || (flag2 && eventCategoryIdentity.Category.StartsWith(text, StringComparison.OrdinalIgnoreCase)) || (flag && flag2 && eventCategoryIdentity.Category.ToLowerInvariant().Contains(text.ToLowerInvariant()))))
								{
									list.Add(eventCategoryObject);
								}
							}
						}
						catch (DataSourceOperationException)
						{
						}
					}
				}
			}
			else
			{
				RegistryKey categoryRegistryKey = EventCategorySession.GetCategoryRegistryKey(categoryId, false, out text4);
				using (categoryRegistryKey)
				{
					if (categoryId.Category == null || flag || flag2)
					{
						string text7 = categoryId.ToString();
						if (flag || flag2)
						{
							text7 = text7.Substring(0, text7.LastIndexOf("\\"));
						}
						if (categoryId.SubEventSource == null)
						{
							foreach (string str in categoryRegistryKey.GetSubKeyNames())
							{
								string text8 = text7 + "\\" + str + "\\";
								if (categoryId.Category != null)
								{
									text8 += categoryId.Category;
								}
								EventCategoryObject[] categories2 = EventCategorySession.GetCategories(EventCategoryIdentity.Parse(text8));
								foreach (EventCategoryObject item in categories2)
								{
									list.Add(item);
								}
							}
						}
						foreach (string text9 in categoryRegistryKey.GetValueNames())
						{
							string text10 = text9.Remove(0, text9.IndexOf(' ') + 1);
							if ((!flag && !flag2) || (flag && text10.EndsWith(text, StringComparison.OrdinalIgnoreCase)) || (flag2 && text10.StartsWith(text, StringComparison.OrdinalIgnoreCase)) || (flag && flag2 && text10.ToLowerInvariant().Contains(text.ToLowerInvariant())))
							{
								list.Add(new EventCategoryObject(text10, Convert.ToInt32(text9.Substring(0, text9.IndexOf(' '))), (ExEventLog.EventLevel)categoryRegistryKey.GetValue(text9), EventCategoryIdentity.Parse(text7 + "\\" + text10)));
							}
						}
					}
					else if (text4 != null)
					{
						list.Add(new EventCategoryObject(text4.Remove(0, text4.IndexOf(' ') + 1), Convert.ToInt32(text4.Substring(0, text4.IndexOf(' '))), (ExEventLog.EventLevel)categoryRegistryKey.GetValue(text4), categoryId));
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0003B0E8 File Offset: 0x000392E8
		private static EventCategoryObject GetCategory(EventCategoryIdentity categoryId)
		{
			if (categoryId.Category == null)
			{
				return null;
			}
			EventCategoryObject[] categories = EventCategorySession.GetCategories(categoryId);
			if (categories.Length > 0)
			{
				return categories[0];
			}
			return null;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0003B114 File Offset: 0x00039314
		private static void SetCategory(EventCategoryObject category)
		{
			string text = null;
			EventCategoryIdentity eventCategoryIdentity = category.Identity as EventCategoryIdentity;
			RegistryKey categoryRegistryKey = EventCategorySession.GetCategoryRegistryKey(eventCategoryIdentity, true, out text);
			using (categoryRegistryKey)
			{
				if (text == null)
				{
					throw new DataSourceOperationException(DataStrings.ExceptionEventCategoryNotFound(eventCategoryIdentity.Category));
				}
				categoryRegistryKey.SetValue(text, (int)category.EventLevel, RegistryValueKind.DWord);
			}
		}

		// Token: 0x04000B48 RID: 2888
		private string source;
	}
}
