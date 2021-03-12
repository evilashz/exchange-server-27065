using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006D9 RID: 1753
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UncSession
	{
		// Token: 0x060045D5 RID: 17877 RVA: 0x00128F88 File Offset: 0x00127188
		private UncSession(UncObjectId objectId, IPrincipal authenticatedUser)
		{
			WindowsPrincipal windowsPrincipal = authenticatedUser as WindowsPrincipal;
			if (windowsPrincipal == null)
			{
				throw new ArgumentException("authenticatedUser");
			}
			this.identity = (WindowsIdentity)windowsPrincipal.Identity;
			this.id = new UncObjectId(new Uri("\\\\" + objectId.Path.Host), UriFlags.Unc);
		}

		// Token: 0x060045D6 RID: 17878 RVA: 0x00128FE8 File Offset: 0x001271E8
		public static UncSession Open(ObjectId objectId, IPrincipal authenticatedUser)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			if (authenticatedUser == null)
			{
				throw new ArgumentNullException("authenticatedUser");
			}
			UncObjectId uncObjectId = objectId as UncObjectId;
			if (uncObjectId == null)
			{
				throw new ArgumentException("objectId");
			}
			return new UncSession(uncObjectId, authenticatedUser);
		}

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x060045D7 RID: 17879 RVA: 0x0012902D File Offset: 0x0012722D
		public ObjectId Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x060045D8 RID: 17880 RVA: 0x00129035 File Offset: 0x00127235
		public string Title
		{
			get
			{
				return this.id.Path.Host;
			}
		}

		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x060045D9 RID: 17881 RVA: 0x00129047 File Offset: 0x00127247
		public Uri Uri
		{
			get
			{
				return this.id.Path;
			}
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x00129054 File Offset: 0x00127254
		public ITableView GetView(QueryFilter filter, SortBy[] sortBy, params PropertyDefinition[] propsToReturn)
		{
			WindowsImpersonationContext windowsImpersonationContext = Utils.ImpersonateUser(this.identity);
			ITableView result;
			try
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					int num = 0;
					int num3;
					int num4;
					int num2 = UncSession.NetShareEnum(this.id.Path.Host, 1, out zero, -1, out num3, out num4, ref num);
					if (num2 == 5)
					{
						throw new AccessDeniedException(this.Id, Strings.ExAccessDenied(this.id.Path.LocalPath));
					}
					if (num2 == 2250 || num2 == 53)
					{
						throw new ObjectMovedOrDeletedException(this.Id, Strings.ExObjectMovedOrDeleted(this.id.Path.LocalPath));
					}
					List<object[]> list = new List<object[]>();
					if (num2 == 0 && num3 > 0)
					{
						int num5 = Marshal.SizeOf(typeof(UncSession.SHARE_INFO_1));
						IntPtr ptr = zero;
						int num6 = 0;
						int num7 = Utils.GetViewMaxRows;
						while (num6 < num3 && num7 > 0)
						{
							UncSession.SHARE_INFO_1 share_INFO_ = (UncSession.SHARE_INFO_1)Marshal.PtrToStructure(ptr, typeof(UncSession.SHARE_INFO_1));
							if (share_INFO_.ShareType == UncSession.ShareType.Disk)
							{
								UncObjectId uncObjectId = new UncObjectId(new Uri(Path.Combine("\\\\" + this.id.Path.Host, share_INFO_.NetName)), UriFlags.UncDocumentLibrary);
								object[] array = new object[propsToReturn.Length];
								DirectoryInfo directoryInfo = null;
								bool flag = true;
								int i = 0;
								while (i < propsToReturn.Length)
								{
									DocumentLibraryPropertyId documentLibraryPropertyId = DocumentLibraryPropertyId.None;
									DocumentLibraryPropertyDefinition documentLibraryPropertyDefinition = propsToReturn[i] as DocumentLibraryPropertyDefinition;
									if (documentLibraryPropertyDefinition != null)
									{
										documentLibraryPropertyId = documentLibraryPropertyDefinition.PropertyId;
									}
									DocumentLibraryPropertyId documentLibraryPropertyId2 = documentLibraryPropertyId;
									switch (documentLibraryPropertyId2)
									{
									case DocumentLibraryPropertyId.None:
										array[i] = new PropertyError(propsToReturn[i], PropertyErrorCode.NotFound);
										break;
									case DocumentLibraryPropertyId.Uri:
										array[i] = uncObjectId.Path;
										break;
									case DocumentLibraryPropertyId.ContentLength:
									case DocumentLibraryPropertyId.CreationTime:
									case DocumentLibraryPropertyId.LastModifiedTime:
									case DocumentLibraryPropertyId.IsFolder:
										goto IL_1F2;
									case DocumentLibraryPropertyId.IsHidden:
										array[i] = share_INFO_.NetName.EndsWith("$");
										break;
									case DocumentLibraryPropertyId.Id:
										array[i] = uncObjectId;
										break;
									case DocumentLibraryPropertyId.Title:
										array[i] = share_INFO_.NetName;
										break;
									default:
										if (documentLibraryPropertyId2 != DocumentLibraryPropertyId.Description)
										{
											goto IL_1F2;
										}
										array[i] = share_INFO_.Remark;
										break;
									}
									IL_248:
									i++;
									continue;
									IL_1F2:
									if (flag)
									{
										try
										{
											if (directoryInfo == null && flag)
											{
												directoryInfo = new DirectoryInfo(uncObjectId.Path.LocalPath);
												FileAttributes attributes = directoryInfo.Attributes;
											}
										}
										catch (UnauthorizedAccessException)
										{
											flag = false;
										}
										catch (IOException)
										{
											flag = false;
										}
									}
									if (flag)
									{
										array[i] = UncDocumentLibraryItem.GetValueFromFileSystemInfo(documentLibraryPropertyDefinition, directoryInfo);
										goto IL_248;
									}
									array[i] = new PropertyError(documentLibraryPropertyDefinition, PropertyErrorCode.NotFound);
									goto IL_248;
								}
								list.Add(array);
								num7--;
							}
							num6++;
							ptr = (IntPtr)(ptr.ToInt64() + (long)num5);
						}
					}
					result = new ArrayTableView(filter, sortBy, propsToReturn, list);
				}
				finally
				{
					if (IntPtr.Zero != zero)
					{
						UncSession.NetApiBufferFree(zero);
					}
				}
			}
			catch
			{
				Utils.UndoContext(ref windowsImpersonationContext);
				throw;
			}
			finally
			{
				Utils.UndoContext(ref windowsImpersonationContext);
			}
			return result;
		}

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x060045DB RID: 17883 RVA: 0x001293A0 File Offset: 0x001275A0
		internal WindowsIdentity Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x060045DC RID: 17884
		[DllImport("netapi32", CharSet = CharSet.Unicode)]
		internal static extern int NetShareEnum(string lpServerName, int dwLevel, out IntPtr lpBuffer, int dwPrefMaxLen, out int entriesRead, out int totalEntries, ref int hResume);

		// Token: 0x060045DD RID: 17885
		[DllImport("netapi32")]
		internal static extern int NetApiBufferFree(IntPtr lpBuffer);

		// Token: 0x060045DE RID: 17886
		[DllImport("netapi32", CharSet = CharSet.Unicode)]
		internal static extern int NetShareGetInfo(string serverName, string netName, int level, out IntPtr lpBuffer);

		// Token: 0x04002634 RID: 9780
		internal const int NO_ERROR = 0;

		// Token: 0x04002635 RID: 9781
		internal const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x04002636 RID: 9782
		internal const int ERROR_NETWORK_PATH_NOT_FOUND = 53;

		// Token: 0x04002637 RID: 9783
		internal const int ERROR_NOT_CONNECTED = 2250;

		// Token: 0x04002638 RID: 9784
		internal const int NERR_NetNameNotFound = 2310;

		// Token: 0x04002639 RID: 9785
		internal const int NERR_DeviceNotShared = 2311;

		// Token: 0x0400263A RID: 9786
		internal const int NERR_ClientNameNotFound = 2312;

		// Token: 0x0400263B RID: 9787
		private readonly WindowsIdentity identity;

		// Token: 0x0400263C RID: 9788
		private readonly UncObjectId id;

		// Token: 0x020006DA RID: 1754
		[Flags]
		internal enum ShareType : uint
		{
			// Token: 0x0400263E RID: 9790
			Disk = 0U,
			// Token: 0x0400263F RID: 9791
			Printer = 1U,
			// Token: 0x04002640 RID: 9792
			Device = 2U,
			// Token: 0x04002641 RID: 9793
			IPC = 3U,
			// Token: 0x04002642 RID: 9794
			Special = 2147483648U
		}

		// Token: 0x020006DB RID: 1755
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct SHARE_INFO_1
		{
			// Token: 0x04002643 RID: 9795
			[MarshalAs(UnmanagedType.LPWStr)]
			public string NetName;

			// Token: 0x04002644 RID: 9796
			public UncSession.ShareType ShareType;

			// Token: 0x04002645 RID: 9797
			[MarshalAs(UnmanagedType.LPWStr)]
			public string Remark;
		}
	}
}
