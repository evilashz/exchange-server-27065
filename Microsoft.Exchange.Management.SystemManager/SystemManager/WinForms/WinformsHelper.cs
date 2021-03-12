using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.MapiTasks;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000114 RID: 276
	public static class WinformsHelper
	{
		// Token: 0x06000A07 RID: 2567 RVA: 0x00022AFC File Offset: 0x00020CFC
		public static void SetDataObjectToClipboard(object data, bool copy)
		{
			try
			{
				Clipboard.SetDataObject(data, copy);
			}
			catch (ExternalException)
			{
			}
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00022B28 File Offset: 0x00020D28
		public static object SetNetName(object dagNetworkId, object netName)
		{
			return new DagNetworkObjectId((string)dagNetworkId)
			{
				NetName = (string)netName
			}.ToString();
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00022B54 File Offset: 0x00020D54
		public static string GetTextFromClipboard()
		{
			string result = string.Empty;
			try
			{
				result = Clipboard.GetText();
			}
			catch (ExternalException)
			{
			}
			return result;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00022B84 File Offset: 0x00020D84
		public static string GetExecutingAssemblyDirectory()
		{
			string location = Assembly.GetExecutingAssembly().Location;
			return Path.GetDirectoryName(location);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00022BA2 File Offset: 0x00020DA2
		public static string GetAbsolutePath(string fileName)
		{
			return WinformsHelper.GetExecutingAssemblyDirectory() + "\\" + fileName;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00022BB4 File Offset: 0x00020DB4
		public static bool IsEmptyValue(object propertyValue)
		{
			bool result = false;
			if (propertyValue == null)
			{
				result = true;
			}
			else if (DBNull.Value.Equals(propertyValue))
			{
				result = true;
			}
			else if (propertyValue is IEnumerable && WinformsHelper.IsEmptyCollection(propertyValue as IEnumerable))
			{
				result = true;
			}
			else if (propertyValue is Guid && Guid.Empty.Equals(propertyValue))
			{
				result = true;
			}
			else if (string.IsNullOrEmpty(propertyValue.ToString()))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00022C28 File Offset: 0x00020E28
		private static bool IsEmptyCollection(IEnumerable enumerable)
		{
			bool result = true;
			if (enumerable != null)
			{
				using (IEnumerator enumerator = enumerable.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00022C7C File Offset: 0x00020E7C
		public static Type GetNullableTypeArgument(Type type)
		{
			Type result = type;
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && type.GetGenericArguments().Length == 1)
			{
				result = type.GetGenericArguments()[0];
			}
			return result;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00022CC0 File Offset: 0x00020EC0
		public static Stream GetManifestResource(DataDrivenCategory dataDrivenCategory)
		{
			string assemblyString;
			string name;
			ManagementGUICommon.GetRegisterAssembly(dataDrivenCategory, ref assemblyString, ref name);
			return Assembly.Load(assemblyString).GetManifestResourceStream(name);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00022CE4 File Offset: 0x00020EE4
		internal static void OpenUrl(Uri url)
		{
			try
			{
				Process.Start(url.OriginalString);
			}
			catch (Win32Exception innerException)
			{
				throw new UrlHandlerNotFoundException(url.OriginalString, innerException);
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00022D20 File Offset: 0x00020F20
		internal static void ShowNonFileUrl(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string uriString = e.Link.LinkData as string;
			try
			{
				Uri uri = new Uri(uriString);
				if (uri.IsFile)
				{
					throw new InvalidOperationException(Strings.NonFileUrlError);
				}
				WinformsHelper.OpenUrl(uri);
			}
			catch (UriFormatException)
			{
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00022D7C File Offset: 0x00020F7C
		internal static Point GetCentralLocation(Size formSize)
		{
			int width = Screen.PrimaryScreen.WorkingArea.Width;
			int height = Screen.PrimaryScreen.WorkingArea.Height;
			return new Point((width - formSize.Width) / 2, (height - formSize.Height) / 2);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00022DCC File Offset: 0x00020FCC
		internal static void ShowExportDialog(IWin32Window owner, DataListView listControl, IUIService uiService)
		{
			using (System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog())
			{
				saveFileDialog.OverwritePrompt = true;
				saveFileDialog.Title = Strings.ExportListFileDialogTitle;
				saveFileDialog.Filter = string.Format("{0}|*.txt|{1}|*.csv|{2}|*.txt|{3}|*.csv", new object[]
				{
					Strings.ExportListFileFilterTextTab,
					Strings.ExportListFileFilterTextComma,
					Strings.ExportListFileFilterUnicodeTab,
					Strings.ExportListFileFilterUnicodeComma
				});
				if (DialogResult.OK == saveFileDialog.ShowDialog(owner))
				{
					Encoding fileEncoding = (saveFileDialog.FilterIndex <= 2) ? Encoding.Default : Encoding.Unicode;
					char separator = (saveFileDialog.FilterIndex % 2 == 1) ? '\t' : ',';
					try
					{
						listControl.ExportListToFile(saveFileDialog.FileName, fileEncoding, separator);
					}
					catch (IOException ex)
					{
						uiService.ShowError(Strings.ExportListFileIOError(ex.Message));
					}
					catch (UnauthorizedAccessException ex2)
					{
						uiService.ShowError(Strings.ExportListFileIOError(ex2.Message));
					}
				}
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00022EF4 File Offset: 0x000210F4
		internal static bool OverrideCorruptedValuesWithDefault(ADObject dataSource)
		{
			bool result = false;
			try
			{
				dataSource.OverrideCorruptedValuesWithDefault();
			}
			catch (Exception)
			{
			}
			finally
			{
				result = (ObjectState.Changed == dataSource.ObjectState);
			}
			return result;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00022F38 File Offset: 0x00021138
		public static string GetDllResourceString(string dllPath, int resourceId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (SafeLibraryHandle safeLibraryHandle = SafeLibraryHandle.LoadLibrary(dllPath))
			{
				if (!safeLibraryHandle.IsInvalid)
				{
					int num = NativeMethods.LoadString(safeLibraryHandle, resourceId, stringBuilder, 0);
					if (num != 0)
					{
						stringBuilder.EnsureCapacity(num + 1);
						NativeMethods.LoadString(safeLibraryHandle, resourceId, stringBuilder, stringBuilder.Capacity);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00022FA4 File Offset: 0x000211A4
		internal static string GenerateFormName<TForm>(string objectId) where TForm : Form
		{
			return string.Format("{0}_{1}", typeof(TForm).Name, objectId);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00022FC0 File Offset: 0x000211C0
		internal static string GenerateFormName<TForm>(ADObjectId objectId) where TForm : Form
		{
			return WinformsHelper.GenerateFormName<TForm>(objectId.ObjectGuid.ToString());
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00022FE8 File Offset: 0x000211E8
		public static bool IsValidRegKey(string path)
		{
			bool result = false;
			RegistryKey registryKey = WinformsHelper.GetRegistryKey(Registry.LocalMachine, path);
			if (registryKey != null)
			{
				result = true;
				registryKey.Close();
			}
			return result;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00023010 File Offset: 0x00021210
		public static bool IsMailToShellCommandAvailable()
		{
			bool result = false;
			RegistryKey registryKey = WinformsHelper.GetRegistryKey(Registry.ClassesRoot, "mailto");
			if (registryKey != null)
			{
				result = (registryKey.GetValue("URL Protocol", null) != null);
				registryKey.Close();
			}
			return result;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002304C File Offset: 0x0002124C
		private static RegistryKey GetRegistryKey(RegistryKey rootRegKey, string path)
		{
			RegistryKey result = null;
			try
			{
				result = rootRegKey.OpenSubKey(path);
			}
			catch (SecurityException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return result;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00023088 File Offset: 0x00021288
		public static bool IsRemoteEnabled()
		{
			if (PSConnectionInfoSingleton.GetInstance().Type != OrganizationType.ToolOrEdge)
			{
				return true;
			}
			if (EnvironmentAnalyzer.IsWorkGroup())
			{
				return false;
			}
			string name = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\AdminTools";
			bool result = true;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue("EMC.RemotePowerShellEnabled");
						if (value != null && string.Equals("false", value.ToString(), StringComparison.OrdinalIgnoreCase))
						{
							result = false;
						}
					}
				}
			}
			catch (SecurityException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return result;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00023124 File Offset: 0x00021324
		public static IList GetAddedList(this MultiValuedPropertyBase mvp)
		{
			return mvp.Added;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002312C File Offset: 0x0002132C
		public static IList GetRemovedList(this MultiValuedPropertyBase mvp)
		{
			return mvp.Removed;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002314C File Offset: 0x0002134C
		public static void InvokeAsync(MethodInvoker callback, IWin32Window owner)
		{
			using (InvisibleForm invisibleForm = new InvisibleForm())
			{
				invisibleForm.BackgroundWorker.DoWork += delegate(object sender, DoWorkEventArgs e2)
				{
					callback();
				};
				invisibleForm.ShowDialog(owner);
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000231B0 File Offset: 0x000213B0
		public static object DeSerialize(byte[] data)
		{
			object result = null;
			if (data != null)
			{
				using (MemoryStream memoryStream = new MemoryStream(data))
				{
					BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null, new string[]
					{
						"System.DelegateSerializationHolder"
					});
					result = binaryFormatter.Deserialize(memoryStream);
				}
			}
			return result;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00023208 File Offset: 0x00021408
		public static byte[] Serialize(object data)
		{
			if (data == null)
			{
				return null;
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
				binaryFormatter.Serialize(memoryStream, data);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00023254 File Offset: 0x00021454
		public static bool ByteArrayEquals(byte[] source, byte[] destination)
		{
			bool result = true;
			if (source.Length == destination.Length)
			{
				for (int i = 0; i < source.Length; i++)
				{
					if (source[i] != destination[i])
					{
						result = false;
						break;
					}
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002328A File Offset: 0x0002148A
		public static ADObjectId GenerateADObjectId(Guid guid, string distinguishedName)
		{
			return new ADObjectId(distinguishedName, guid);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00023294 File Offset: 0x00021494
		public static string GetADShortName(string fullName)
		{
			string result = string.Empty;
			if (!string.IsNullOrEmpty(fullName))
			{
				int num = fullName.LastIndexOf('/');
				result = ((num < 0) ? fullName : fullName.Substring(num + 1, fullName.Length - num - 1));
			}
			return result;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x000232D4 File Offset: 0x000214D4
		public static string GetFQDNShortName(object fqdn)
		{
			return WinformsHelper.GetServerShortName(fqdn as string);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x000232E4 File Offset: 0x000214E4
		public static string CalculateMoveRequestTypeForMailbox(object mailboxMoveStatus, object mailboxMoveFlags, object mailboxMoveRemoteHostName)
		{
			RequestStatus requestStatus = (RequestStatus)mailboxMoveStatus;
			RequestFlags requestFlags = (RequestFlags)mailboxMoveFlags;
			if (requestStatus != RequestStatus.None && (requestFlags & RequestFlags.IntraOrg) != RequestFlags.None)
			{
				return Strings.MoveRequestTypeIntraOrg;
			}
			if ((requestStatus == RequestStatus.None && !string.IsNullOrEmpty(mailboxMoveRemoteHostName as string)) || (requestStatus != RequestStatus.None && (requestFlags & RequestFlags.CrossOrg) != RequestFlags.None))
			{
				return Strings.MoveRequestTypeCrossOrg;
			}
			return null;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00023338 File Offset: 0x00021538
		public static string CalculateMoveRequestTypeForMigration(object mailboxMoveFlags)
		{
			RequestFlags requestFlags = WinformsHelper.ParseRequestFlags(mailboxMoveFlags.ToString());
			return ((requestFlags & RequestFlags.CrossOrg) != RequestFlags.None) ? Strings.MoveRequestTypeCrossOrg : Strings.MoveRequestTypeIntraOrg;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00023368 File Offset: 0x00021568
		public static MailboxMoveType CalcuateSupportedMailboxMoveType(object recipientType, object archiveState)
		{
			MailboxMoveType result = MailboxMoveType.None;
			RecipientType recipientType2 = (RecipientType)recipientType;
			if (recipientType2 == RecipientType.MailUser || recipientType2 == RecipientType.UserMailbox)
			{
				bool flag = (ArchiveState)archiveState == ArchiveState.Local;
				switch (recipientType2)
				{
				case RecipientType.UserMailbox:
					result = (flag ? MailboxMoveType.BothUserAndArchive : MailboxMoveType.OnlyUserMailbox);
					break;
				case RecipientType.MailUser:
					if (flag)
					{
						result = MailboxMoveType.OnlyArchiveMailbox;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x000233B4 File Offset: 0x000215B4
		public static object CalculateCanNewMoveRequest(object recipientType, object mailboxMoveStatus, object mailboxMoveFlags, object mailboxMoveRemoteHostName, object archiveState)
		{
			bool flag = false;
			RecipientType recipientType2 = (RecipientType)recipientType;
			if ((recipientType2 == RecipientType.MailUser || recipientType2 == RecipientType.UserMailbox) && string.IsNullOrEmpty(WinformsHelper.CalculateMoveRequestTypeForMailbox(mailboxMoveStatus, mailboxMoveFlags, mailboxMoveRemoteHostName)))
			{
				flag = (recipientType2 == RecipientType.UserMailbox || ((ArchiveState)archiveState == ArchiveState.Local && WinformsHelper.IsCloudOrganization()));
			}
			return flag;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00023404 File Offset: 0x00021604
		public static DataTableLoaderView SetDataSource(DataListView dataListView, UIPresentationProfile uiPresentationProfile, DataTableLoader dataTableLoader)
		{
			DataTableLoaderView dataTableLoaderView = (dataTableLoader == null) ? null : DataTableLoaderView.Create(dataTableLoader);
			WinformsHelper.SetDataSource(dataListView, uiPresentationProfile, dataTableLoaderView);
			return dataTableLoaderView;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00023444 File Offset: 0x00021644
		public static IList GetSelectedVirtualDirectory(IList directories, IList servers)
		{
			if (directories == null || servers == null)
			{
				return null;
			}
			return (from dir in directories.OfType<ADVirtualDirectory>()
			where servers.Contains(dir.Server)
			select dir).ToArray<ADVirtualDirectory>();
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00023488 File Offset: 0x00021688
		public static FilteredDataTableLoaderView SetFilteredDataSource(DataListView dataListView, UIPresentationProfile uiPresentationProfile, DataTableLoader dataTableLoader)
		{
			FilteredDataTableLoaderView filteredDataTableLoaderView = (dataTableLoader == null) ? null : FilteredDataTableLoaderView.Create(dataTableLoader);
			WinformsHelper.SetDataSource(dataListView, uiPresentationProfile, filteredDataTableLoaderView);
			return filteredDataTableLoaderView;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000234AC File Offset: 0x000216AC
		public static void SetDataSource(DataListView dataListView, UIPresentationProfile uiPresentationProfile, DataTableLoaderView dataTableLoaderView)
		{
			if (dataListView == null)
			{
				throw new ArgumentNullException("dataListView");
			}
			if (!WinformsHelper.CheckDataSource(dataListView.DataSource))
			{
				throw new ArgumentException("dataListView");
			}
			if (dataListView.DataSource != null)
			{
				DataTableLoaderView dataTableLoaderView2 = (dataListView.DataSource as AdvancedBindingSource).DataSource as DataTableLoaderView;
				dataListView.DataSource = null;
				if (dataTableLoaderView2 != null)
				{
					dataTableLoaderView2.Dispose();
				}
			}
			if (dataTableLoaderView != null)
			{
				WinformsHelper.SyncSortSupportDescriptions(dataListView, uiPresentationProfile, dataTableLoaderView);
				dataListView.DataSource = new AdvancedBindingSource
				{
					DataSource = dataTableLoaderView
				};
			}
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002352C File Offset: 0x0002172C
		public static DataTableLoaderView SetDataSource(ObjectList objectList, UIPresentationProfile uiPresentationProfile, DataTableLoader dataTableLoader)
		{
			DataTableLoaderView dataTableLoaderView = (dataTableLoader == null) ? null : DataTableLoaderView.Create(dataTableLoader);
			WinformsHelper.SetDataSource(objectList, uiPresentationProfile, dataTableLoaderView);
			return dataTableLoaderView;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00023550 File Offset: 0x00021750
		public static FilteredDataTableLoaderView SetFilteredDataSource(ObjectList objectList, UIPresentationProfile uiPresentationProfile, DataTableLoader dataTableLoader)
		{
			FilteredDataTableLoaderView filteredDataTableLoaderView = (dataTableLoader == null) ? null : FilteredDataTableLoaderView.Create(dataTableLoader);
			WinformsHelper.SetDataSource(objectList, uiPresentationProfile, filteredDataTableLoaderView);
			return filteredDataTableLoaderView;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00023574 File Offset: 0x00021774
		public static void SetDataSource(ObjectList objectList, UIPresentationProfile uiPresentationProfile, DataTableLoaderView dataTableLoaderView)
		{
			if (objectList == null)
			{
				throw new ArgumentNullException("objectList");
			}
			if (!WinformsHelper.CheckDataSource(objectList.DataSource))
			{
				throw new ArgumentException("objectList");
			}
			if (objectList.DataSource != null)
			{
				DataTableLoaderView dataTableLoaderView2 = (objectList.DataSource as AdvancedBindingSource).DataSource as DataTableLoaderView;
				objectList.DataSource = null;
				if (dataTableLoaderView2 != null)
				{
					dataTableLoaderView2.Dispose();
				}
			}
			if (dataTableLoaderView != null)
			{
				WinformsHelper.SyncSortSupportDescriptions(objectList.ListView, uiPresentationProfile, dataTableLoaderView);
				objectList.DataSource = new AdvancedBindingSource
				{
					DataSource = dataTableLoaderView
				};
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x000235F9 File Offset: 0x000217F9
		private static bool CheckDataSource(object dataSource)
		{
			return dataSource == null || (dataSource is AdvancedBindingSource && ((dataSource as AdvancedBindingSource).DataSource == null || (dataSource as AdvancedBindingSource).DataSource is DataTableLoaderView));
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00023650 File Offset: 0x00021850
		private static void SyncSortSupportDescriptions(DataListView dataListView, UIPresentationProfile uiPresentationProfile, DataTableLoaderView dataTableLoaderView)
		{
			if (uiPresentationProfile != null)
			{
				foreach (ResultsColumnProfile resultsColumnProfile in uiPresentationProfile.DisplayedColumnCollection)
				{
					dataTableLoaderView.SortSupportDescriptions.Add(new SortSupportDescription(resultsColumnProfile.Name, resultsColumnProfile.SortMode, resultsColumnProfile.CustomComparer, resultsColumnProfile.CustomFormatter, resultsColumnProfile.FormatProvider, resultsColumnProfile.FormatString, resultsColumnProfile.DefaultEmptyText));
				}
			}
			using (IEnumerator<ExchangeColumnHeader> enumerator = dataListView.AvailableColumns.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ExchangeColumnHeader columnHeader = enumerator.Current;
					if (columnHeader.IsSortable)
					{
						if (!dataTableLoaderView.SortSupportDescriptions.Any((SortSupportDescription obj) => string.Compare(obj.ColumnName, columnHeader.Name, StringComparison.OrdinalIgnoreCase) == 0))
						{
							dataTableLoaderView.SortSupportDescriptions.Add(new SortSupportDescription(columnHeader.Name, SortMode.NotSpecified, null, columnHeader.CustomFormatter, columnHeader.FormatProvider, columnHeader.FormatString, columnHeader.DefaultEmptyText));
						}
					}
				}
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00023780 File Offset: 0x00021980
		public static UMLanguageListSource ConvertUMLanguageListToUMLanguageListSource(IList umLanguageList)
		{
			return new UMLanguageListSource((umLanguageList == null) ? new UMLanguage[0] : umLanguageList.Cast<UMLanguage>().ToArray<UMLanguage>());
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000237A0 File Offset: 0x000219A0
		public static object FindItemAndGetPropertyValue(IList items, string keyPropertyName, object keyPropertyValue, string valuePropertyName)
		{
			foreach (object obj in items)
			{
				PropertyInfo property = obj.GetType().GetProperty(keyPropertyName);
				PropertyInfo property2 = obj.GetType().GetProperty(valuePropertyName);
				if (keyPropertyValue.Equals(property.GetValue(obj, null)))
				{
					return property2.GetValue(obj, null);
				}
			}
			return null;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00023828 File Offset: 0x00021A28
		public static IList GetAddedFederatedDomains(object originalAccountNS, object accountNS, object federatedDomains)
		{
			SmtpDomain rhs = originalAccountNS as SmtpDomain;
			SmtpDomain smtpDomain = accountNS as SmtpDomain;
			MultiValuedProperty<FederatedDomain> multiValuedProperty = federatedDomains as MultiValuedProperty<FederatedDomain>;
			List<SmtpDomain> list = new List<SmtpDomain>();
			if (multiValuedProperty != null && smtpDomain != null)
			{
				IList list2 = multiValuedProperty.Added;
				if (!smtpDomain.Equals(rhs))
				{
					list2 = multiValuedProperty;
				}
				foreach (object obj in list2)
				{
					FederatedDomain federatedDomain = (FederatedDomain)obj;
					if (!federatedDomain.Domain.Equals(smtpDomain))
					{
						list.Add(federatedDomain.Domain);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000238D8 File Offset: 0x00021AD8
		public static IList GetRemovedFederatedDomains(object originalAccountNS, object accountNS, object federatedDomains)
		{
			SmtpDomain smtpDomain = originalAccountNS as SmtpDomain;
			SmtpDomain objB = accountNS as SmtpDomain;
			MultiValuedProperty<FederatedDomain> multiValuedProperty = federatedDomains as MultiValuedProperty<FederatedDomain>;
			List<SmtpDomain> list = new List<SmtpDomain>();
			if (multiValuedProperty != null && smtpDomain != null)
			{
				foreach (FederatedDomain federatedDomain in multiValuedProperty.Removed)
				{
					if (!federatedDomain.Domain.Equals(smtpDomain))
					{
						list.Add(federatedDomain.Domain);
					}
				}
				if (!object.Equals(smtpDomain, objB))
				{
					foreach (FederatedDomain federatedDomain2 in multiValuedProperty)
					{
						if (!federatedDomain2.Domain.Equals(smtpDomain) && !WinformsHelper.FederatedDomainsContains(multiValuedProperty.Added, federatedDomain2.Domain))
						{
							list.Add(federatedDomain2.Domain);
						}
					}
					list.Add(smtpDomain);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x000239D4 File Offset: 0x00021BD4
		private static bool FederatedDomainsContains(object[] fedDomainsList, SmtpDomain domain)
		{
			bool result = false;
			if (fedDomainsList != null)
			{
				foreach (FederatedDomain federatedDomain in fedDomainsList)
				{
					if (domain.Equals(federatedDomain.Domain))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00023A14 File Offset: 0x00021C14
		public static string GenerateFederationDNSRecordExample(object acceptedDomains, object appID)
		{
			IList list = acceptedDomains as IList;
			string arg = appID as string;
			string arg2 = "example.com";
			if (list != null)
			{
				foreach (object obj in list)
				{
					AcceptedDomain acceptedDomain = (AcceptedDomain)obj;
					if (acceptedDomain.DomainType == AcceptedDomainType.Authoritative)
					{
						arg2 = acceptedDomain.DomainName.Domain;
						if (acceptedDomain.Default)
						{
							break;
						}
					}
				}
			}
			return string.Format("{0} IN TXT AppID={1}", arg2, arg);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00023AA8 File Offset: 0x00021CA8
		public static MultiValuedProperty<FederatedDomain> GenerateMvpFederatedDomains(object fedDomains)
		{
			MultiValuedProperty<FederatedDomain> multiValuedProperty = new MultiValuedProperty<FederatedDomain>();
			if (!fedDomains.IsNullValue())
			{
				foreach (object obj in ((IList)fedDomains))
				{
					FederatedDomain item = (FederatedDomain)obj;
					multiValuedProperty.Add(item);
				}
			}
			multiValuedProperty.ResetChangeTracking();
			return multiValuedProperty;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00023B18 File Offset: 0x00021D18
		public static MultiValuedProperty<ADObjectId> GetDagServers(object dagList, object dagIdentity)
		{
			if (dagList == null)
			{
				throw new ArgumentNullException("dagList");
			}
			if (!(dagList is IList))
			{
				throw new ArgumentException("dagList");
			}
			if (dagIdentity == null)
			{
				throw new ArgumentNullException("dagIdentity");
			}
			MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>();
			foreach (object obj in ((IList)dagList))
			{
				DatabaseAvailabilityGroup databaseAvailabilityGroup = (DatabaseAvailabilityGroup)obj;
				if (databaseAvailabilityGroup != null && databaseAvailabilityGroup.Identity.Equals(dagIdentity))
				{
					using (MultiValuedProperty<ADObjectId>.Enumerator enumerator2 = databaseAvailabilityGroup.Servers.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ADObjectId item = enumerator2.Current;
							multiValuedProperty.Add(item);
						}
						break;
					}
				}
			}
			multiValuedProperty.ResetChangeTracking();
			return multiValuedProperty;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00023C00 File Offset: 0x00021E00
		public static ADObjectId[] GetServersInDag(object dagList, object dagIdentity)
		{
			if (dagList == null)
			{
				throw new ArgumentNullException("dagList");
			}
			if (!(dagList is IList))
			{
				throw new ArgumentException("dagList");
			}
			if (dagIdentity == null)
			{
				throw new ArgumentNullException("dagIdentity");
			}
			List<ADObjectId> list = new List<ADObjectId>();
			foreach (object obj in ((IList)dagList))
			{
				DatabaseAvailabilityGroup databaseAvailabilityGroup = (DatabaseAvailabilityGroup)obj;
				if (databaseAvailabilityGroup != null && !databaseAvailabilityGroup.Identity.Equals(dagIdentity))
				{
					list.AddRange(databaseAvailabilityGroup.Servers);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00023CAC File Offset: 0x00021EAC
		public static string GetTargetTypeToStringValue(object obj, Type targetType)
		{
			if (obj == null)
			{
				return null;
			}
			if (targetType.IsEnum && !obj.IsNullValue())
			{
				return Enum.ToObject(targetType, obj).ToString();
			}
			return obj.ToString();
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00023CD8 File Offset: 0x00021ED8
		public static object GetPropertyValue(object component, PropertyDescriptor propertyDescriptor)
		{
			object obj = propertyDescriptor.GetValue(component);
			if (obj != null && DBNull.Value != obj && propertyDescriptor.PropertyType.IsEnum)
			{
				obj = Enum.ToObject(propertyDescriptor.PropertyType, obj);
			}
			return obj;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00023D14 File Offset: 0x00021F14
		public static string ConvertValueToString(DataColumn orignalColumn, object rawValue)
		{
			if (orignalColumn.DataType.IsEnum && !DBNull.Value.Equals(rawValue))
			{
				rawValue = Enum.ToObject(orignalColumn.DataType, rawValue);
			}
			ICustomTextConverter customTextConverter = (orignalColumn.ExtendedProperties["TextConverter"] as ICustomTextConverter) ?? TextConverter.DefaultConverter;
			return customTextConverter.Format(null, rawValue, null);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00023D74 File Offset: 0x00021F74
		public static Icon GetIconFromIconLibrary(string iconName)
		{
			PropertyInfo property = typeof(Icons).GetProperty(iconName, BindingFlags.Static | BindingFlags.Public);
			if (!(property != null))
			{
				return null;
			}
			return (Icon)property.GetValue(null, null);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00023DAC File Offset: 0x00021FAC
		internal static bool IsConnectedWithLocalServer()
		{
			string localServerName = EnvironmentAnalyzer.GetLocalServerName();
			return WinformsHelper.IsCurrentConnectedServer(localServerName);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00023DC8 File Offset: 0x00021FC8
		internal static bool IsCurrentConnectedServer(string serverFqdn)
		{
			string currentConnectedServerName = WinformsHelper.GetCurrentConnectedServerName();
			return string.Equals(currentConnectedServerName, serverFqdn, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00023DE4 File Offset: 0x00021FE4
		internal static string GetCurrentConnectedServerName()
		{
			string result = string.Empty;
			if (WinformsHelper.IsRemoteEnabled())
			{
				result = PSConnectionInfoSingleton.GetInstance().ServerName;
			}
			else
			{
				result = EnvironmentAnalyzer.GetLocalServerName();
			}
			return result;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00023E14 File Offset: 0x00022014
		public static string GetServerShortName(string serverName)
		{
			if (string.IsNullOrEmpty(serverName))
			{
				return string.Empty;
			}
			int num = serverName.IndexOf(".", StringComparison.OrdinalIgnoreCase);
			if (num < 0)
			{
				return serverName;
			}
			return serverName.Substring(0, num);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00023ED0 File Offset: 0x000220D0
		public static void BindRadioButtonToOptionValue<T>(AutoHeightRadioButton radioButton, T optionValue, object dataSource, string dataMember)
		{
			if (radioButton == null)
			{
				throw new ArgumentNullException("radioButton");
			}
			Binding dataBinding = radioButton.DataBindings.Add("Checked", dataSource, dataMember, true, DataSourceUpdateMode.Never);
			dataBinding.Format += delegate(object sender, ConvertEventArgs e)
			{
				e.Value = (!e.Value.IsNullValue() && optionValue.Equals((T)((object)e.Value)));
			};
			dataBinding.Parse += delegate(object sender, ConvertEventArgs e)
			{
				e.Value = optionValue;
				dataBinding.DataSourceUpdateMode = DataSourceUpdateMode.Never;
			};
			radioButton.CheckedChangedRaising += delegate(object sender, HandledEventArgs e)
			{
				dataBinding.DataSourceUpdateMode = (radioButton.Checked ? DataSourceUpdateMode.OnPropertyChanged : DataSourceUpdateMode.Never);
			};
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00023F88 File Offset: 0x00022188
		public static bool HasLocalArchive(object mailboxes)
		{
			DataTable dataTable = mailboxes as DataTable;
			if (dataTable == null)
			{
				return false;
			}
			return (from q in dataTable.AsEnumerable()
			select q["ArchiveDatabase"]).Any((object db) => !DBNull.Value.Equals(db));
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00023FEC File Offset: 0x000221EC
		public static string GenerateInternalUrl(object vdir, object serverFqdn)
		{
			string result = string.Empty;
			string text = serverFqdn as string;
			if (!DBNull.Value.Equals(vdir) && !string.IsNullOrEmpty(text))
			{
				Dictionary<Type, string> dictionary = new Dictionary<Type, string>
				{
					{
						typeof(ADOwaVirtualDirectory),
						"https://" + text + "/owa"
					},
					{
						typeof(ADEcpVirtualDirectory),
						"https://" + text + "/ecp"
					},
					{
						typeof(ADWebServicesVirtualDirectory),
						string.Empty
					},
					{
						typeof(ADMobileVirtualDirectory),
						string.Empty
					},
					{
						typeof(ADAutodiscoverVirtualDirectory),
						string.Empty
					},
					{
						typeof(ADOabVirtualDirectory),
						string.Empty
					}
				};
				result = dictionary[vdir.GetType()];
			}
			return result;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x000240CC File Offset: 0x000222CC
		public static string GenerateResetVdirCmdlet(string verb, object vdir)
		{
			if (!DBNull.Value.Equals(vdir))
			{
				Dictionary<Type, string> dictionary = new Dictionary<Type, string>
				{
					{
						typeof(ADOwaVirtualDirectory),
						"OwaVirtualDirectory"
					},
					{
						typeof(ADEcpVirtualDirectory),
						"EcpVirtualDirectory"
					},
					{
						typeof(ADWebServicesVirtualDirectory),
						"WebServicesVirtualDirectory"
					},
					{
						typeof(ADMobileVirtualDirectory),
						"ActiveSyncVirtualDirectory"
					},
					{
						typeof(ADAutodiscoverVirtualDirectory),
						"AutodiscoverVirtualDirectory"
					},
					{
						typeof(ADOabVirtualDirectory),
						"OabVirtualDirectory"
					}
				};
				return string.Format("{0}-{1}", verb, dictionary[vdir.GetType()]);
			}
			return string.Empty;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002418C File Offset: 0x0002238C
		public static string GetWebSiteNameForVdir(object vdir)
		{
			string result = string.Empty;
			if (!DBNull.Value.Equals(vdir))
			{
				string name = (vdir as ADObject).Name;
				Regex regex = new Regex("\\(.*\\)");
				Match match = regex.Match(name);
				if (!match.Success)
				{
					throw new ArgumentException(string.Format("{0} is not a valid virtual directory name", name));
				}
				result = match.Value.Trim(new char[]
				{
					'(',
					')'
				});
			}
			return result;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00024208 File Offset: 0x00022408
		public static object GetIdentityForVdir(object vdir)
		{
			ADObject adobject = vdir as ADObject;
			if (adobject == null)
			{
				return null;
			}
			return adobject.Identity;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00024227 File Offset: 0x00022427
		public static MultiValuedProperty<PublicFolderAccessRight> GetPublicFolderAccessRights(object publicFolderPermission)
		{
			return PublicFolderAccessRight.CreatePublicFolderAccessRightCollection((PublicFolderPermission)publicFolderPermission);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00024234 File Offset: 0x00022434
		public static bool IsCloudOrganization()
		{
			return PSConnectionInfoSingleton.GetInstance().Type == OrganizationType.Cloud;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00024243 File Offset: 0x00022443
		public static bool IsCurrentOrganizationAllowed(IList<OrganizationType> organizationTypeList)
		{
			return organizationTypeList == null || organizationTypeList.Count == 0 || organizationTypeList.Contains(PSConnectionInfoSingleton.GetInstance().Type);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00024264 File Offset: 0x00022464
		public static bool IsCmdletAllowedInScope(string commandText, string parameter)
		{
			return EMCRunspaceConfigurationSingleton.GetInstance().IsCmdletAllowedInScope(commandText, new string[]
			{
				parameter
			});
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00024288 File Offset: 0x00022488
		public static string NewCertificateSubjectKeyIdentifier()
		{
			return Guid.NewGuid().ToString("N");
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x000242A8 File Offset: 0x000224A8
		public static IList GetFederationCertificateDomainName()
		{
			MultiValuedProperty<SmtpDomainWithSubdomains> multiValuedProperty = new MultiValuedProperty<SmtpDomainWithSubdomains>();
			multiValuedProperty.Add(new SmtpDomainWithSubdomains("Federation", false));
			multiValuedProperty.ResetChangeTracking();
			return multiValuedProperty;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000242D3 File Offset: 0x000224D3
		public static bool IsWin7OrLater()
		{
			return Environment.OSVersion.Version.Major >= 7 || (Environment.OSVersion.Version.Major >= 6 && Environment.OSVersion.Version.Minor >= 1);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00024314 File Offset: 0x00022514
		public static object GetRegistryKeyValue(string keyPath, string valueName)
		{
			object result = null;
			try
			{
				result = Registry.GetValue(keyPath, valueName, null);
			}
			catch (SecurityException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return result;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00024354 File Offset: 0x00022554
		public static bool IsIDCRLNotReady()
		{
			if (WinformsHelper.IsWin7OrLater())
			{
				string keyPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\MSOIdentityCRL";
				return null == WinformsHelper.GetRegistryKeyValue(keyPath, "MSOIDCRLVersion");
			}
			return false;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002437E File Offset: 0x0002257E
		public static RequestStatus ParseRequestStatus(string status)
		{
			return (RequestStatus)Enum.Parse(typeof(RequestStatus), status);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00024395 File Offset: 0x00022595
		public static RequestFlags ParseRequestFlags(string status)
		{
			return (RequestFlags)Enum.Parse(typeof(RequestFlags), status);
		}
	}
}
