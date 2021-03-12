using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002B6 RID: 694
	[Serializable]
	internal sealed class FileIOAccess
	{
		// Token: 0x060024E2 RID: 9442 RVA: 0x00086744 File Offset: 0x00084944
		public FileIOAccess()
		{
			this.m_set = new StringExpressionSet(this.m_ignoreCase, true);
			this.m_allFiles = false;
			this.m_allLocalFiles = false;
			this.m_pathDiscovery = false;
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x0008677A File Offset: 0x0008497A
		public FileIOAccess(bool pathDiscovery)
		{
			this.m_set = new StringExpressionSet(this.m_ignoreCase, true);
			this.m_allFiles = false;
			this.m_allLocalFiles = false;
			this.m_pathDiscovery = pathDiscovery;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000867B0 File Offset: 0x000849B0
		[SecurityCritical]
		public FileIOAccess(string value)
		{
			if (value == null)
			{
				this.m_set = new StringExpressionSet(this.m_ignoreCase, true);
				this.m_allFiles = false;
				this.m_allLocalFiles = false;
			}
			else if (value.Length >= "*AllFiles*".Length && string.Compare("*AllFiles*", value, StringComparison.Ordinal) == 0)
			{
				this.m_set = new StringExpressionSet(this.m_ignoreCase, true);
				this.m_allFiles = true;
				this.m_allLocalFiles = false;
			}
			else if (value.Length >= "*AllLocalFiles*".Length && string.Compare("*AllLocalFiles*", 0, value, 0, "*AllLocalFiles*".Length, StringComparison.Ordinal) == 0)
			{
				this.m_set = new StringExpressionSet(this.m_ignoreCase, value.Substring("*AllLocalFiles*".Length), true);
				this.m_allFiles = false;
				this.m_allLocalFiles = true;
			}
			else
			{
				this.m_set = new StringExpressionSet(this.m_ignoreCase, value, true);
				this.m_allFiles = false;
				this.m_allLocalFiles = false;
			}
			this.m_pathDiscovery = false;
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000868BA File Offset: 0x00084ABA
		public FileIOAccess(bool allFiles, bool allLocalFiles, bool pathDiscovery)
		{
			this.m_set = new StringExpressionSet(this.m_ignoreCase, true);
			this.m_allFiles = allFiles;
			this.m_allLocalFiles = allLocalFiles;
			this.m_pathDiscovery = pathDiscovery;
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000868F0 File Offset: 0x00084AF0
		public FileIOAccess(StringExpressionSet set, bool allFiles, bool allLocalFiles, bool pathDiscovery)
		{
			this.m_set = set;
			this.m_set.SetThrowOnRelative(true);
			this.m_allFiles = allFiles;
			this.m_allLocalFiles = allLocalFiles;
			this.m_pathDiscovery = pathDiscovery;
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x00086928 File Offset: 0x00084B28
		private FileIOAccess(FileIOAccess operand)
		{
			this.m_set = operand.m_set.Copy();
			this.m_allFiles = operand.m_allFiles;
			this.m_allLocalFiles = operand.m_allLocalFiles;
			this.m_pathDiscovery = operand.m_pathDiscovery;
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x00086977 File Offset: 0x00084B77
		[SecurityCritical]
		public void AddExpressions(ArrayList values, bool checkForDuplicates)
		{
			this.m_allFiles = false;
			this.m_set.AddExpressions(values, checkForDuplicates);
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060024E9 RID: 9449 RVA: 0x0008698D File Offset: 0x00084B8D
		// (set) Token: 0x060024EA RID: 9450 RVA: 0x00086995 File Offset: 0x00084B95
		public bool AllFiles
		{
			get
			{
				return this.m_allFiles;
			}
			set
			{
				this.m_allFiles = value;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x0008699E File Offset: 0x00084B9E
		// (set) Token: 0x060024EC RID: 9452 RVA: 0x000869A6 File Offset: 0x00084BA6
		public bool AllLocalFiles
		{
			get
			{
				return this.m_allLocalFiles;
			}
			set
			{
				this.m_allLocalFiles = value;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (set) Token: 0x060024ED RID: 9453 RVA: 0x000869AF File Offset: 0x00084BAF
		public bool PathDiscovery
		{
			set
			{
				this.m_pathDiscovery = value;
			}
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000869B8 File Offset: 0x00084BB8
		public bool IsEmpty()
		{
			return !this.m_allFiles && !this.m_allLocalFiles && (this.m_set == null || this.m_set.IsEmpty());
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000869E1 File Offset: 0x00084BE1
		public FileIOAccess Copy()
		{
			return new FileIOAccess(this);
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000869EC File Offset: 0x00084BEC
		[SecuritySafeCritical]
		public FileIOAccess Union(FileIOAccess operand)
		{
			if (operand == null)
			{
				if (!this.IsEmpty())
				{
					return this.Copy();
				}
				return null;
			}
			else
			{
				if (this.m_allFiles || operand.m_allFiles)
				{
					return new FileIOAccess(true, false, this.m_pathDiscovery);
				}
				return new FileIOAccess(this.m_set.Union(operand.m_set), false, this.m_allLocalFiles || operand.m_allLocalFiles, this.m_pathDiscovery);
			}
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x00086A5C File Offset: 0x00084C5C
		[SecuritySafeCritical]
		public FileIOAccess Intersect(FileIOAccess operand)
		{
			if (operand == null)
			{
				return null;
			}
			if (this.m_allFiles)
			{
				if (operand.m_allFiles)
				{
					return new FileIOAccess(true, false, this.m_pathDiscovery);
				}
				return new FileIOAccess(operand.m_set.Copy(), false, operand.m_allLocalFiles, this.m_pathDiscovery);
			}
			else
			{
				if (operand.m_allFiles)
				{
					return new FileIOAccess(this.m_set.Copy(), false, this.m_allLocalFiles, this.m_pathDiscovery);
				}
				StringExpressionSet stringExpressionSet = new StringExpressionSet(this.m_ignoreCase, true);
				if (this.m_allLocalFiles)
				{
					string[] array = operand.m_set.UnsafeToStringArray();
					if (array != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							string root = FileIOAccess.GetRoot(array[i]);
							if (root != null && FileIOAccess.IsLocalDrive(FileIOAccess.GetRoot(root)))
							{
								stringExpressionSet.AddExpressions(new string[]
								{
									array[i]
								}, true, false);
							}
						}
					}
				}
				if (operand.m_allLocalFiles)
				{
					string[] array2 = this.m_set.UnsafeToStringArray();
					if (array2 != null)
					{
						for (int j = 0; j < array2.Length; j++)
						{
							string root2 = FileIOAccess.GetRoot(array2[j]);
							if (root2 != null && FileIOAccess.IsLocalDrive(FileIOAccess.GetRoot(root2)))
							{
								stringExpressionSet.AddExpressions(new string[]
								{
									array2[j]
								}, true, false);
							}
						}
					}
				}
				string[] array3 = this.m_set.Intersect(operand.m_set).UnsafeToStringArray();
				if (array3 != null)
				{
					stringExpressionSet.AddExpressions(array3, !stringExpressionSet.IsEmpty(), false);
				}
				return new FileIOAccess(stringExpressionSet, false, this.m_allLocalFiles && operand.m_allLocalFiles, this.m_pathDiscovery);
			}
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x00086BDC File Offset: 0x00084DDC
		[SecuritySafeCritical]
		public bool IsSubsetOf(FileIOAccess operand)
		{
			if (operand == null)
			{
				return this.IsEmpty();
			}
			if (operand.m_allFiles)
			{
				return true;
			}
			if ((!this.m_pathDiscovery || !this.m_set.IsSubsetOfPathDiscovery(operand.m_set)) && !this.m_set.IsSubsetOf(operand.m_set))
			{
				if (!operand.m_allLocalFiles)
				{
					return false;
				}
				string[] array = this.m_set.UnsafeToStringArray();
				for (int i = 0; i < array.Length; i++)
				{
					string root = FileIOAccess.GetRoot(array[i]);
					if (root == null || !FileIOAccess.IsLocalDrive(FileIOAccess.GetRoot(root)))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x00086C70 File Offset: 0x00084E70
		private static string GetRoot(string path)
		{
			string text = path.Substring(0, 3);
			if (text.EndsWith(":\\", StringComparison.Ordinal))
			{
				return text;
			}
			return null;
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x00086C98 File Offset: 0x00084E98
		[SecuritySafeCritical]
		public override string ToString()
		{
			if (this.m_allFiles)
			{
				return "*AllFiles*";
			}
			if (this.m_allLocalFiles)
			{
				string text = "*AllLocalFiles*";
				string text2 = this.m_set.UnsafeToString();
				if (text2 != null && text2.Length > 0)
				{
					text = text + ";" + text2;
				}
				return text;
			}
			return this.m_set.UnsafeToString();
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x00086CF3 File Offset: 0x00084EF3
		[SecuritySafeCritical]
		public string[] ToStringArray()
		{
			return this.m_set.UnsafeToStringArray();
		}

		// Token: 0x060024F6 RID: 9462
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool IsLocalDrive(string path);

		// Token: 0x060024F7 RID: 9463 RVA: 0x00086D00 File Offset: 0x00084F00
		[SecuritySafeCritical]
		public override bool Equals(object obj)
		{
			FileIOAccess fileIOAccess = obj as FileIOAccess;
			if (fileIOAccess == null)
			{
				return this.IsEmpty() && obj == null;
			}
			if (this.m_pathDiscovery)
			{
				return (this.m_allFiles && fileIOAccess.m_allFiles) || (this.m_allLocalFiles == fileIOAccess.m_allLocalFiles && this.m_set.IsSubsetOf(fileIOAccess.m_set) && fileIOAccess.m_set.IsSubsetOf(this.m_set));
			}
			return this.IsSubsetOf(fileIOAccess) && fileIOAccess.IsSubsetOf(this);
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x00086D8F File Offset: 0x00084F8F
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000DCF RID: 3535
		private bool m_ignoreCase = true;

		// Token: 0x04000DD0 RID: 3536
		private StringExpressionSet m_set;

		// Token: 0x04000DD1 RID: 3537
		private bool m_allFiles;

		// Token: 0x04000DD2 RID: 3538
		private bool m_allLocalFiles;

		// Token: 0x04000DD3 RID: 3539
		private bool m_pathDiscovery;

		// Token: 0x04000DD4 RID: 3540
		private const string m_strAllFiles = "*AllFiles*";

		// Token: 0x04000DD5 RID: 3541
		private const string m_strAllLocalFiles = "*AllLocalFiles*";
	}
}
