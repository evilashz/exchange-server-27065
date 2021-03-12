using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002B5 RID: 693
	[ComVisible(true)]
	[Serializable]
	public sealed class FileIOPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x060024B7 RID: 9399 RVA: 0x00085488 File Offset: 0x00083688
		public FileIOPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_unrestricted = true;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_unrestricted = false;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000854B8 File Offset: 0x000836B8
		[SecuritySafeCritical]
		public FileIOPermission(FileIOPermissionAccess access, string path)
		{
			FileIOPermission.VerifyAccess(access);
			string[] pathListOrig = new string[]
			{
				path
			};
			this.AddPathList(access, pathListOrig, false, true, false);
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000854E7 File Offset: 0x000836E7
		[SecuritySafeCritical]
		public FileIOPermission(FileIOPermissionAccess access, string[] pathList)
		{
			FileIOPermission.VerifyAccess(access);
			this.AddPathList(access, pathList, false, true, false);
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x00085500 File Offset: 0x00083700
		[SecuritySafeCritical]
		public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string path)
		{
			FileIOPermission.VerifyAccess(access);
			string[] pathListOrig = new string[]
			{
				path
			};
			this.AddPathList(access, control, pathListOrig, false, true, false);
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x00085530 File Offset: 0x00083730
		[SecuritySafeCritical]
		public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string[] pathList) : this(access, control, pathList, true, true)
		{
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x0008553D File Offset: 0x0008373D
		[SecurityCritical]
		internal FileIOPermission(FileIOPermissionAccess access, string[] pathList, bool checkForDuplicates, bool needFullPath)
		{
			FileIOPermission.VerifyAccess(access);
			this.AddPathList(access, pathList, checkForDuplicates, needFullPath, true);
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x00085557 File Offset: 0x00083757
		[SecurityCritical]
		internal FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string[] pathList, bool checkForDuplicates, bool needFullPath)
		{
			FileIOPermission.VerifyAccess(access);
			this.AddPathList(access, control, pathList, checkForDuplicates, needFullPath, true);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x00085574 File Offset: 0x00083774
		public void SetPathList(FileIOPermissionAccess access, string path)
		{
			string[] pathList;
			if (path == null)
			{
				pathList = new string[0];
			}
			else
			{
				pathList = new string[]
				{
					path
				};
			}
			this.SetPathList(access, pathList, false);
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000855A1 File Offset: 0x000837A1
		public void SetPathList(FileIOPermissionAccess access, string[] pathList)
		{
			this.SetPathList(access, pathList, true);
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000855AC File Offset: 0x000837AC
		internal void SetPathList(FileIOPermissionAccess access, string[] pathList, bool checkForDuplicates)
		{
			this.SetPathList(access, AccessControlActions.None, pathList, checkForDuplicates);
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000855B8 File Offset: 0x000837B8
		[SecuritySafeCritical]
		internal void SetPathList(FileIOPermissionAccess access, AccessControlActions control, string[] pathList, bool checkForDuplicates)
		{
			FileIOPermission.VerifyAccess(access);
			if ((access & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
			{
				this.m_read = null;
			}
			if ((access & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
			{
				this.m_write = null;
			}
			if ((access & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
			{
				this.m_append = null;
			}
			if ((access & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
			{
				this.m_pathDiscovery = null;
			}
			if ((control & AccessControlActions.View) != AccessControlActions.None)
			{
				this.m_viewAcl = null;
			}
			if ((control & AccessControlActions.Change) != AccessControlActions.None)
			{
				this.m_changeAcl = null;
			}
			this.m_unrestricted = false;
			this.AddPathList(access, control, pathList, checkForDuplicates, true, true);
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x00085628 File Offset: 0x00083828
		[SecuritySafeCritical]
		public void AddPathList(FileIOPermissionAccess access, string path)
		{
			string[] pathListOrig;
			if (path == null)
			{
				pathListOrig = new string[0];
			}
			else
			{
				pathListOrig = new string[]
				{
					path
				};
			}
			this.AddPathList(access, pathListOrig, false, true, false);
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x00085657 File Offset: 0x00083857
		[SecuritySafeCritical]
		public void AddPathList(FileIOPermissionAccess access, string[] pathList)
		{
			this.AddPathList(access, pathList, true, true, true);
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x00085664 File Offset: 0x00083864
		[SecurityCritical]
		internal void AddPathList(FileIOPermissionAccess access, string[] pathListOrig, bool checkForDuplicates, bool needFullPath, bool copyPathList)
		{
			this.AddPathList(access, AccessControlActions.None, pathListOrig, checkForDuplicates, needFullPath, copyPathList);
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x00085674 File Offset: 0x00083874
		[SecurityCritical]
		internal void AddPathList(FileIOPermissionAccess access, AccessControlActions control, string[] pathListOrig, bool checkForDuplicates, bool needFullPath, bool copyPathList)
		{
			if (pathListOrig == null)
			{
				throw new ArgumentNullException("pathList");
			}
			if (pathListOrig.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			FileIOPermission.VerifyAccess(access);
			if (this.m_unrestricted)
			{
				return;
			}
			string[] array = pathListOrig;
			if (copyPathList)
			{
				array = new string[pathListOrig.Length];
				Array.Copy(pathListOrig, array, pathListOrig.Length);
			}
			FileIOPermission.CheckIllegalCharacters(array, needFullPath);
			ArrayList values = StringExpressionSet.CreateListFromExpressions(array, needFullPath);
			if ((access & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
			{
				if (this.m_read == null)
				{
					this.m_read = new FileIOAccess();
				}
				this.m_read.AddExpressions(values, checkForDuplicates);
			}
			if ((access & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
			{
				if (this.m_write == null)
				{
					this.m_write = new FileIOAccess();
				}
				this.m_write.AddExpressions(values, checkForDuplicates);
			}
			if ((access & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
			{
				if (this.m_append == null)
				{
					this.m_append = new FileIOAccess();
				}
				this.m_append.AddExpressions(values, checkForDuplicates);
			}
			if ((access & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
			{
				if (this.m_pathDiscovery == null)
				{
					this.m_pathDiscovery = new FileIOAccess(true);
				}
				this.m_pathDiscovery.AddExpressions(values, checkForDuplicates);
			}
			if ((control & AccessControlActions.View) != AccessControlActions.None)
			{
				if (this.m_viewAcl == null)
				{
					this.m_viewAcl = new FileIOAccess();
				}
				this.m_viewAcl.AddExpressions(values, checkForDuplicates);
			}
			if ((control & AccessControlActions.Change) != AccessControlActions.None)
			{
				if (this.m_changeAcl == null)
				{
					this.m_changeAcl = new FileIOAccess();
				}
				this.m_changeAcl.AddExpressions(values, checkForDuplicates);
			}
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000857C4 File Offset: 0x000839C4
		[SecuritySafeCritical]
		public string[] GetPathList(FileIOPermissionAccess access)
		{
			FileIOPermission.VerifyAccess(access);
			FileIOPermission.ExclusiveAccess(access);
			if (FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.Read))
			{
				if (this.m_read == null)
				{
					return null;
				}
				return this.m_read.ToStringArray();
			}
			else if (FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.Write))
			{
				if (this.m_write == null)
				{
					return null;
				}
				return this.m_write.ToStringArray();
			}
			else if (FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.Append))
			{
				if (this.m_append == null)
				{
					return null;
				}
				return this.m_append.ToStringArray();
			}
			else
			{
				if (!FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.PathDiscovery))
				{
					return null;
				}
				if (this.m_pathDiscovery == null)
				{
					return null;
				}
				return this.m_pathDiscovery.ToStringArray();
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060024C7 RID: 9415 RVA: 0x0008585C File Offset: 0x00083A5C
		// (set) Token: 0x060024C8 RID: 9416 RVA: 0x000858DC File Offset: 0x00083ADC
		public FileIOPermissionAccess AllLocalFiles
		{
			get
			{
				if (this.m_unrestricted)
				{
					return FileIOPermissionAccess.AllAccess;
				}
				FileIOPermissionAccess fileIOPermissionAccess = FileIOPermissionAccess.NoAccess;
				if (this.m_read != null && this.m_read.AllLocalFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Read;
				}
				if (this.m_write != null && this.m_write.AllLocalFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Write;
				}
				if (this.m_append != null && this.m_append.AllLocalFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Append;
				}
				if (this.m_pathDiscovery != null && this.m_pathDiscovery.AllLocalFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.PathDiscovery;
				}
				return fileIOPermissionAccess;
			}
			set
			{
				if ((value & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_read == null)
					{
						this.m_read = new FileIOAccess();
					}
					this.m_read.AllLocalFiles = true;
				}
				else if (this.m_read != null)
				{
					this.m_read.AllLocalFiles = false;
				}
				if ((value & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_write == null)
					{
						this.m_write = new FileIOAccess();
					}
					this.m_write.AllLocalFiles = true;
				}
				else if (this.m_write != null)
				{
					this.m_write.AllLocalFiles = false;
				}
				if ((value & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_append == null)
					{
						this.m_append = new FileIOAccess();
					}
					this.m_append.AllLocalFiles = true;
				}
				else if (this.m_append != null)
				{
					this.m_append.AllLocalFiles = false;
				}
				if ((value & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_pathDiscovery == null)
					{
						this.m_pathDiscovery = new FileIOAccess(true);
					}
					this.m_pathDiscovery.AllLocalFiles = true;
					return;
				}
				if (this.m_pathDiscovery != null)
				{
					this.m_pathDiscovery.AllLocalFiles = false;
				}
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060024C9 RID: 9417 RVA: 0x000859D4 File Offset: 0x00083BD4
		// (set) Token: 0x060024CA RID: 9418 RVA: 0x00085A54 File Offset: 0x00083C54
		public FileIOPermissionAccess AllFiles
		{
			get
			{
				if (this.m_unrestricted)
				{
					return FileIOPermissionAccess.AllAccess;
				}
				FileIOPermissionAccess fileIOPermissionAccess = FileIOPermissionAccess.NoAccess;
				if (this.m_read != null && this.m_read.AllFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Read;
				}
				if (this.m_write != null && this.m_write.AllFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Write;
				}
				if (this.m_append != null && this.m_append.AllFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Append;
				}
				if (this.m_pathDiscovery != null && this.m_pathDiscovery.AllFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.PathDiscovery;
				}
				return fileIOPermissionAccess;
			}
			set
			{
				if (value == FileIOPermissionAccess.AllAccess)
				{
					this.m_unrestricted = true;
					return;
				}
				if ((value & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_read == null)
					{
						this.m_read = new FileIOAccess();
					}
					this.m_read.AllFiles = true;
				}
				else if (this.m_read != null)
				{
					this.m_read.AllFiles = false;
				}
				if ((value & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_write == null)
					{
						this.m_write = new FileIOAccess();
					}
					this.m_write.AllFiles = true;
				}
				else if (this.m_write != null)
				{
					this.m_write.AllFiles = false;
				}
				if ((value & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_append == null)
					{
						this.m_append = new FileIOAccess();
					}
					this.m_append.AllFiles = true;
				}
				else if (this.m_append != null)
				{
					this.m_append.AllFiles = false;
				}
				if ((value & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_pathDiscovery == null)
					{
						this.m_pathDiscovery = new FileIOAccess(true);
					}
					this.m_pathDiscovery.AllFiles = true;
					return;
				}
				if (this.m_pathDiscovery != null)
				{
					this.m_pathDiscovery.AllFiles = false;
				}
			}
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x00085B56 File Offset: 0x00083D56
		private static void VerifyAccess(FileIOPermissionAccess access)
		{
			if ((access & ~(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write | FileIOPermissionAccess.Append | FileIOPermissionAccess.PathDiscovery)) != FileIOPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)access
				}));
			}
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x00085B7D File Offset: 0x00083D7D
		private static void ExclusiveAccess(FileIOPermissionAccess access)
		{
			if (access == FileIOPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
			if ((access & access - 1) != FileIOPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x00085BAC File Offset: 0x00083DAC
		private static void CheckIllegalCharacters(string[] str, bool onlyCheckExtras)
		{
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == null)
				{
					throw new ArgumentNullException("path");
				}
				if (FileIOPermission.CheckExtraPathCharacters(str[i]))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
				}
				if (!onlyCheckExtras)
				{
					Path.CheckInvalidPathChars(str[i], false);
				}
			}
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x00085C00 File Offset: 0x00083E00
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool CheckExtraPathCharacters(string path)
		{
			bool flag = CodeAccessSecurityEngine.QuickCheckForAllDemands() && !AppContextSwitches.UseLegacyPathHandling;
			int num = (!flag) ? 0 : (PathInternal.IsDevice(path) ? 4 : 0);
			for (int i = num; i < path.Length; i++)
			{
				char c = path[i];
				if (c == '*' || c == '?' || c == '\0')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x00085C5C File Offset: 0x00083E5C
		private static bool AccessIsSet(FileIOPermissionAccess access, FileIOPermissionAccess question)
		{
			return (access & question) > FileIOPermissionAccess.NoAccess;
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x00085C64 File Offset: 0x00083E64
		private bool IsEmpty()
		{
			return !this.m_unrestricted && (this.m_read == null || this.m_read.IsEmpty()) && (this.m_write == null || this.m_write.IsEmpty()) && (this.m_append == null || this.m_append.IsEmpty()) && (this.m_pathDiscovery == null || this.m_pathDiscovery.IsEmpty()) && (this.m_viewAcl == null || this.m_viewAcl.IsEmpty()) && (this.m_changeAcl == null || this.m_changeAcl.IsEmpty());
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x00085CF9 File Offset: 0x00083EF9
		public bool IsUnrestricted()
		{
			return this.m_unrestricted;
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x00085D04 File Offset: 0x00083F04
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			FileIOPermission fileIOPermission = target as FileIOPermission;
			if (fileIOPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			return fileIOPermission.IsUnrestricted() || (!this.IsUnrestricted() && ((this.m_read == null || this.m_read.IsSubsetOf(fileIOPermission.m_read)) && (this.m_write == null || this.m_write.IsSubsetOf(fileIOPermission.m_write)) && (this.m_append == null || this.m_append.IsSubsetOf(fileIOPermission.m_append)) && (this.m_pathDiscovery == null || this.m_pathDiscovery.IsSubsetOf(fileIOPermission.m_pathDiscovery)) && (this.m_viewAcl == null || this.m_viewAcl.IsSubsetOf(fileIOPermission.m_viewAcl))) && (this.m_changeAcl == null || this.m_changeAcl.IsSubsetOf(fileIOPermission.m_changeAcl)));
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x00085E04 File Offset: 0x00084004
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			FileIOPermission fileIOPermission = target as FileIOPermission;
			if (fileIOPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			if (this.IsUnrestricted())
			{
				return target.Copy();
			}
			if (fileIOPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			FileIOAccess fileIOAccess = (this.m_read == null) ? null : this.m_read.Intersect(fileIOPermission.m_read);
			FileIOAccess fileIOAccess2 = (this.m_write == null) ? null : this.m_write.Intersect(fileIOPermission.m_write);
			FileIOAccess fileIOAccess3 = (this.m_append == null) ? null : this.m_append.Intersect(fileIOPermission.m_append);
			FileIOAccess fileIOAccess4 = (this.m_pathDiscovery == null) ? null : this.m_pathDiscovery.Intersect(fileIOPermission.m_pathDiscovery);
			FileIOAccess fileIOAccess5 = (this.m_viewAcl == null) ? null : this.m_viewAcl.Intersect(fileIOPermission.m_viewAcl);
			FileIOAccess fileIOAccess6 = (this.m_changeAcl == null) ? null : this.m_changeAcl.Intersect(fileIOPermission.m_changeAcl);
			if ((fileIOAccess == null || fileIOAccess.IsEmpty()) && (fileIOAccess2 == null || fileIOAccess2.IsEmpty()) && (fileIOAccess3 == null || fileIOAccess3.IsEmpty()) && (fileIOAccess4 == null || fileIOAccess4.IsEmpty()) && (fileIOAccess5 == null || fileIOAccess5.IsEmpty()) && (fileIOAccess6 == null || fileIOAccess6.IsEmpty()))
			{
				return null;
			}
			return new FileIOPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = fileIOAccess,
				m_write = fileIOAccess2,
				m_append = fileIOAccess3,
				m_pathDiscovery = fileIOAccess4,
				m_viewAcl = fileIOAccess5,
				m_changeAcl = fileIOAccess6
			};
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x00085FA4 File Offset: 0x000841A4
		public override IPermission Union(IPermission other)
		{
			if (other == null)
			{
				return this.Copy();
			}
			FileIOPermission fileIOPermission = other as FileIOPermission;
			if (fileIOPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			if (this.IsUnrestricted() || fileIOPermission.IsUnrestricted())
			{
				return new FileIOPermission(PermissionState.Unrestricted);
			}
			FileIOAccess fileIOAccess = (this.m_read == null) ? fileIOPermission.m_read : this.m_read.Union(fileIOPermission.m_read);
			FileIOAccess fileIOAccess2 = (this.m_write == null) ? fileIOPermission.m_write : this.m_write.Union(fileIOPermission.m_write);
			FileIOAccess fileIOAccess3 = (this.m_append == null) ? fileIOPermission.m_append : this.m_append.Union(fileIOPermission.m_append);
			FileIOAccess fileIOAccess4 = (this.m_pathDiscovery == null) ? fileIOPermission.m_pathDiscovery : this.m_pathDiscovery.Union(fileIOPermission.m_pathDiscovery);
			FileIOAccess fileIOAccess5 = (this.m_viewAcl == null) ? fileIOPermission.m_viewAcl : this.m_viewAcl.Union(fileIOPermission.m_viewAcl);
			FileIOAccess fileIOAccess6 = (this.m_changeAcl == null) ? fileIOPermission.m_changeAcl : this.m_changeAcl.Union(fileIOPermission.m_changeAcl);
			if ((fileIOAccess == null || fileIOAccess.IsEmpty()) && (fileIOAccess2 == null || fileIOAccess2.IsEmpty()) && (fileIOAccess3 == null || fileIOAccess3.IsEmpty()) && (fileIOAccess4 == null || fileIOAccess4.IsEmpty()) && (fileIOAccess5 == null || fileIOAccess5.IsEmpty()) && (fileIOAccess6 == null || fileIOAccess6.IsEmpty()))
			{
				return null;
			}
			return new FileIOPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = fileIOAccess,
				m_write = fileIOAccess2,
				m_append = fileIOAccess3,
				m_pathDiscovery = fileIOAccess4,
				m_viewAcl = fileIOAccess5,
				m_changeAcl = fileIOAccess6
			};
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x00086160 File Offset: 0x00084360
		public override IPermission Copy()
		{
			FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.None);
			if (this.m_unrestricted)
			{
				fileIOPermission.m_unrestricted = true;
			}
			else
			{
				fileIOPermission.m_unrestricted = false;
				if (this.m_read != null)
				{
					fileIOPermission.m_read = this.m_read.Copy();
				}
				if (this.m_write != null)
				{
					fileIOPermission.m_write = this.m_write.Copy();
				}
				if (this.m_append != null)
				{
					fileIOPermission.m_append = this.m_append.Copy();
				}
				if (this.m_pathDiscovery != null)
				{
					fileIOPermission.m_pathDiscovery = this.m_pathDiscovery.Copy();
				}
				if (this.m_viewAcl != null)
				{
					fileIOPermission.m_viewAcl = this.m_viewAcl.Copy();
				}
				if (this.m_changeAcl != null)
				{
					fileIOPermission.m_changeAcl = this.m_changeAcl.Copy();
				}
			}
			return fileIOPermission;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x00086228 File Offset: 0x00084428
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.FileIOPermission");
			if (!this.IsUnrestricted())
			{
				if (this.m_read != null && !this.m_read.IsEmpty())
				{
					securityElement.AddAttribute("Read", SecurityElement.Escape(this.m_read.ToString()));
				}
				if (this.m_write != null && !this.m_write.IsEmpty())
				{
					securityElement.AddAttribute("Write", SecurityElement.Escape(this.m_write.ToString()));
				}
				if (this.m_append != null && !this.m_append.IsEmpty())
				{
					securityElement.AddAttribute("Append", SecurityElement.Escape(this.m_append.ToString()));
				}
				if (this.m_pathDiscovery != null && !this.m_pathDiscovery.IsEmpty())
				{
					securityElement.AddAttribute("PathDiscovery", SecurityElement.Escape(this.m_pathDiscovery.ToString()));
				}
				if (this.m_viewAcl != null && !this.m_viewAcl.IsEmpty())
				{
					securityElement.AddAttribute("ViewAcl", SecurityElement.Escape(this.m_viewAcl.ToString()));
				}
				if (this.m_changeAcl != null && !this.m_changeAcl.IsEmpty())
				{
					securityElement.AddAttribute("ChangeAcl", SecurityElement.Escape(this.m_changeAcl.ToString()));
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x00086380 File Offset: 0x00084580
		[SecuritySafeCritical]
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.m_unrestricted = true;
				return;
			}
			this.m_unrestricted = false;
			string text = esd.Attribute("Read");
			if (text != null)
			{
				this.m_read = new FileIOAccess(text);
			}
			else
			{
				this.m_read = null;
			}
			text = esd.Attribute("Write");
			if (text != null)
			{
				this.m_write = new FileIOAccess(text);
			}
			else
			{
				this.m_write = null;
			}
			text = esd.Attribute("Append");
			if (text != null)
			{
				this.m_append = new FileIOAccess(text);
			}
			else
			{
				this.m_append = null;
			}
			text = esd.Attribute("PathDiscovery");
			if (text != null)
			{
				this.m_pathDiscovery = new FileIOAccess(text);
				this.m_pathDiscovery.PathDiscovery = true;
			}
			else
			{
				this.m_pathDiscovery = null;
			}
			text = esd.Attribute("ViewAcl");
			if (text != null)
			{
				this.m_viewAcl = new FileIOAccess(text);
			}
			else
			{
				this.m_viewAcl = null;
			}
			text = esd.Attribute("ChangeAcl");
			if (text != null)
			{
				this.m_changeAcl = new FileIOAccess(text);
				return;
			}
			this.m_changeAcl = null;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x0008648E File Offset: 0x0008468E
		int IBuiltInPermission.GetTokenIndex()
		{
			return FileIOPermission.GetTokenIndex();
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x00086495 File Offset: 0x00084695
		internal static int GetTokenIndex()
		{
			return 2;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x00086498 File Offset: 0x00084698
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			FileIOPermission fileIOPermission = obj as FileIOPermission;
			if (fileIOPermission == null)
			{
				return false;
			}
			if (this.m_unrestricted && fileIOPermission.m_unrestricted)
			{
				return true;
			}
			if (this.m_unrestricted != fileIOPermission.m_unrestricted)
			{
				return false;
			}
			if (this.m_read == null)
			{
				if (fileIOPermission.m_read != null && !fileIOPermission.m_read.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_read.Equals(fileIOPermission.m_read))
			{
				return false;
			}
			if (this.m_write == null)
			{
				if (fileIOPermission.m_write != null && !fileIOPermission.m_write.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_write.Equals(fileIOPermission.m_write))
			{
				return false;
			}
			if (this.m_append == null)
			{
				if (fileIOPermission.m_append != null && !fileIOPermission.m_append.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_append.Equals(fileIOPermission.m_append))
			{
				return false;
			}
			if (this.m_pathDiscovery == null)
			{
				if (fileIOPermission.m_pathDiscovery != null && !fileIOPermission.m_pathDiscovery.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_pathDiscovery.Equals(fileIOPermission.m_pathDiscovery))
			{
				return false;
			}
			if (this.m_viewAcl == null)
			{
				if (fileIOPermission.m_viewAcl != null && !fileIOPermission.m_viewAcl.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_viewAcl.Equals(fileIOPermission.m_viewAcl))
			{
				return false;
			}
			if (this.m_changeAcl == null)
			{
				if (fileIOPermission.m_changeAcl != null && !fileIOPermission.m_changeAcl.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_changeAcl.Equals(fileIOPermission.m_changeAcl))
			{
				return false;
			}
			return true;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x0008660C File Offset: 0x0008480C
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x00086614 File Offset: 0x00084814
		[SecuritySafeCritical]
		internal static void QuickDemand(FileIOPermissionAccess access, string fullPath, bool checkForDuplicates = false, bool needFullPath = true)
		{
			if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				new FileIOPermission(access, new string[]
				{
					fullPath
				}, checkForDuplicates, needFullPath).Demand();
				return;
			}
			FileIOPermission.EmulateFileIOPermissionChecks(fullPath);
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x0008663C File Offset: 0x0008483C
		[SecuritySafeCritical]
		internal static void QuickDemand(FileIOPermissionAccess access, string[] fullPathList, bool checkForDuplicates = false, bool needFullPath = true)
		{
			if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				new FileIOPermission(access, fullPathList, checkForDuplicates, needFullPath).Demand();
				return;
			}
			foreach (string fullPath in fullPathList)
			{
				FileIOPermission.EmulateFileIOPermissionChecks(fullPath);
			}
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x00086679 File Offset: 0x00084879
		[SecuritySafeCritical]
		internal static void QuickDemand(PermissionState state)
		{
			if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				new FileIOPermission(state).Demand();
			}
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x0008668D File Offset: 0x0008488D
		[SecuritySafeCritical]
		internal static void QuickDemand(FileIOPermissionAccess access, AccessControlActions control, string fullPath, bool checkForDuplicates = false, bool needFullPath = true)
		{
			if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				new FileIOPermission(access, control, new string[]
				{
					fullPath
				}, checkForDuplicates, needFullPath).Demand();
				return;
			}
			FileIOPermission.EmulateFileIOPermissionChecks(fullPath);
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000866B8 File Offset: 0x000848B8
		[SecuritySafeCritical]
		internal static void QuickDemand(FileIOPermissionAccess access, AccessControlActions control, string[] fullPathList, bool checkForDuplicates = true, bool needFullPath = true)
		{
			if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				new FileIOPermission(access, control, fullPathList, checkForDuplicates, needFullPath).Demand();
				return;
			}
			foreach (string fullPath in fullPathList)
			{
				FileIOPermission.EmulateFileIOPermissionChecks(fullPath);
			}
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000866F8 File Offset: 0x000848F8
		internal static void EmulateFileIOPermissionChecks(string fullPath)
		{
			if (AppContextSwitches.UseLegacyPathHandling || !PathInternal.IsDevice(fullPath))
			{
				if (PathInternal.HasWildCardCharacters(fullPath))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
				}
				if (PathInternal.HasInvalidVolumeSeparator(fullPath))
				{
					throw new NotSupportedException(Environment.GetResourceString("Argument_PathFormatNotSupported"));
				}
			}
		}

		// Token: 0x04000DC8 RID: 3528
		private FileIOAccess m_read;

		// Token: 0x04000DC9 RID: 3529
		private FileIOAccess m_write;

		// Token: 0x04000DCA RID: 3530
		private FileIOAccess m_append;

		// Token: 0x04000DCB RID: 3531
		private FileIOAccess m_pathDiscovery;

		// Token: 0x04000DCC RID: 3532
		[OptionalField(VersionAdded = 2)]
		private FileIOAccess m_viewAcl;

		// Token: 0x04000DCD RID: 3533
		[OptionalField(VersionAdded = 2)]
		private FileIOAccess m_changeAcl;

		// Token: 0x04000DCE RID: 3534
		private bool m_unrestricted;
	}
}
