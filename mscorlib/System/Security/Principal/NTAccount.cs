using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Principal
{
	// Token: 0x0200030A RID: 778
	[ComVisible(false)]
	public sealed class NTAccount : IdentityReference
	{
		// Token: 0x060027F4 RID: 10228 RVA: 0x00092B34 File Offset: 0x00090D34
		public NTAccount(string domainName, string accountName)
		{
			if (accountName == null)
			{
				throw new ArgumentNullException("accountName");
			}
			if (accountName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StringZeroLength"), "accountName");
			}
			if (accountName.Length > 256)
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_AccountNameTooLong"), "accountName");
			}
			if (domainName != null && domainName.Length > 255)
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_DomainNameTooLong"), "domainName");
			}
			if (domainName == null || domainName.Length == 0)
			{
				this._Name = accountName;
				return;
			}
			this._Name = domainName + "\\" + accountName;
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x00092BE0 File Offset: 0x00090DE0
		public NTAccount(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StringZeroLength"), "name");
			}
			if (name.Length > 512)
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_AccountNameTooLong"), "name");
			}
			this._Name = name;
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x00092C47 File Offset: 0x00090E47
		public override string Value
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x00092C4F File Offset: 0x00090E4F
		public override bool IsValidTargetType(Type targetType)
		{
			return targetType == typeof(SecurityIdentifier) || targetType == typeof(NTAccount);
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x00092C7C File Offset: 0x00090E7C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public override IdentityReference Translate(Type targetType)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (targetType == typeof(NTAccount))
			{
				return this;
			}
			if (targetType == typeof(SecurityIdentifier))
			{
				IdentityReferenceCollection identityReferenceCollection = NTAccount.Translate(new IdentityReferenceCollection(1)
				{
					this
				}, targetType, true);
				return identityReferenceCollection[0];
			}
			throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), "targetType");
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x00092CF8 File Offset: 0x00090EF8
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			NTAccount ntaccount = o as NTAccount;
			return !(ntaccount == null) && this == ntaccount;
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x00092D23 File Offset: 0x00090F23
		public override int GetHashCode()
		{
			return StringComparer.InvariantCultureIgnoreCase.GetHashCode(this._Name);
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x00092D35 File Offset: 0x00090F35
		public override string ToString()
		{
			return this._Name;
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x00092D40 File Offset: 0x00090F40
		[SecurityCritical]
		internal static IdentityReferenceCollection Translate(IdentityReferenceCollection sourceAccounts, Type targetType, bool forceSuccess)
		{
			bool flag = false;
			IdentityReferenceCollection identityReferenceCollection = NTAccount.Translate(sourceAccounts, targetType, out flag);
			if (forceSuccess && flag)
			{
				IdentityReferenceCollection identityReferenceCollection2 = new IdentityReferenceCollection();
				foreach (IdentityReference identityReference in identityReferenceCollection)
				{
					if (identityReference.GetType() != targetType)
					{
						identityReferenceCollection2.Add(identityReference);
					}
				}
				throw new IdentityNotMappedException(Environment.GetResourceString("IdentityReference_IdentityNotMapped"), identityReferenceCollection2);
			}
			return identityReferenceCollection;
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x00092DC4 File Offset: 0x00090FC4
		[SecurityCritical]
		internal static IdentityReferenceCollection Translate(IdentityReferenceCollection sourceAccounts, Type targetType, out bool someFailed)
		{
			if (sourceAccounts == null)
			{
				throw new ArgumentNullException("sourceAccounts");
			}
			if (targetType == typeof(SecurityIdentifier))
			{
				return NTAccount.TranslateToSids(sourceAccounts, out someFailed);
			}
			throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), "targetType");
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x00092E04 File Offset: 0x00091004
		public static bool operator ==(NTAccount left, NTAccount right)
		{
			return (left == null && right == null) || (left != null && right != null && left.ToString().Equals(right.ToString(), StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x00092E37 File Offset: 0x00091037
		public static bool operator !=(NTAccount left, NTAccount right)
		{
			return !(left == right);
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x00092E44 File Offset: 0x00091044
		[SecurityCritical]
		private static IdentityReferenceCollection TranslateToSids(IdentityReferenceCollection sourceAccounts, out bool someFailed)
		{
			if (sourceAccounts == null)
			{
				throw new ArgumentNullException("sourceAccounts");
			}
			if (sourceAccounts.Count == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EmptyCollection"), "sourceAccounts");
			}
			SafeLsaPolicyHandle safeLsaPolicyHandle = SafeLsaPolicyHandle.InvalidHandle;
			SafeLsaMemoryHandle invalidHandle = SafeLsaMemoryHandle.InvalidHandle;
			SafeLsaMemoryHandle invalidHandle2 = SafeLsaMemoryHandle.InvalidHandle;
			IdentityReferenceCollection result;
			try
			{
				Win32Native.UNICODE_STRING[] array = new Win32Native.UNICODE_STRING[sourceAccounts.Count];
				int num = 0;
				foreach (IdentityReference identityReference in sourceAccounts)
				{
					NTAccount ntaccount = identityReference as NTAccount;
					if (ntaccount == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_ImproperType"), "sourceAccounts");
					}
					array[num].Buffer = ntaccount.ToString();
					if (array[num].Buffer.Length * 2 + 2 > 65535)
					{
						throw new SystemException();
					}
					array[num].Length = (ushort)(array[num].Buffer.Length * 2);
					array[num].MaximumLength = array[num].Length + 2;
					num++;
				}
				safeLsaPolicyHandle = Win32.LsaOpenPolicy(null, PolicyRights.POLICY_LOOKUP_NAMES);
				someFailed = false;
				uint num2;
				if (Win32.LsaLookupNames2Supported)
				{
					num2 = Win32Native.LsaLookupNames2(safeLsaPolicyHandle, 0, sourceAccounts.Count, array, ref invalidHandle, ref invalidHandle2);
				}
				else
				{
					num2 = Win32Native.LsaLookupNames(safeLsaPolicyHandle, sourceAccounts.Count, array, ref invalidHandle, ref invalidHandle2);
				}
				if (num2 == 3221225495U || num2 == 3221225626U)
				{
					throw new OutOfMemoryException();
				}
				if (num2 == 3221225506U)
				{
					throw new UnauthorizedAccessException();
				}
				if (num2 == 3221225587U || num2 == 263U)
				{
					someFailed = true;
				}
				else if (num2 != 0U)
				{
					int errorCode = Win32Native.LsaNtStatusToWinError((int)num2);
					throw new SystemException(Win32Native.GetMessage(errorCode));
				}
				IdentityReferenceCollection identityReferenceCollection = new IdentityReferenceCollection(sourceAccounts.Count);
				if (num2 == 0U || num2 == 263U)
				{
					if (Win32.LsaLookupNames2Supported)
					{
						invalidHandle2.Initialize((uint)sourceAccounts.Count, (uint)Marshal.SizeOf(typeof(Win32Native.LSA_TRANSLATED_SID2)));
						Win32.InitializeReferencedDomainsPointer(invalidHandle);
						Win32Native.LSA_TRANSLATED_SID2[] array2 = new Win32Native.LSA_TRANSLATED_SID2[sourceAccounts.Count];
						invalidHandle2.ReadArray<Win32Native.LSA_TRANSLATED_SID2>(0UL, array2, 0, array2.Length);
						int i = 0;
						while (i < sourceAccounts.Count)
						{
							Win32Native.LSA_TRANSLATED_SID2 lsa_TRANSLATED_SID = array2[i];
							switch (lsa_TRANSLATED_SID.Use)
							{
							case 1:
							case 2:
							case 4:
							case 5:
							case 9:
								identityReferenceCollection.Add(new SecurityIdentifier(lsa_TRANSLATED_SID.Sid, true));
								break;
							case 3:
							case 6:
							case 7:
							case 8:
								goto IL_282;
							default:
								goto IL_282;
							}
							IL_294:
							i++;
							continue;
							IL_282:
							someFailed = true;
							identityReferenceCollection.Add(sourceAccounts[i]);
							goto IL_294;
						}
					}
					else
					{
						invalidHandle2.Initialize((uint)sourceAccounts.Count, (uint)Marshal.SizeOf(typeof(Win32Native.LSA_TRANSLATED_SID)));
						Win32.InitializeReferencedDomainsPointer(invalidHandle);
						Win32Native.LSA_REFERENCED_DOMAIN_LIST lsa_REFERENCED_DOMAIN_LIST = invalidHandle.Read<Win32Native.LSA_REFERENCED_DOMAIN_LIST>(0UL);
						SecurityIdentifier[] array3 = new SecurityIdentifier[lsa_REFERENCED_DOMAIN_LIST.Entries];
						for (int j = 0; j < lsa_REFERENCED_DOMAIN_LIST.Entries; j++)
						{
							Win32Native.LSA_TRUST_INFORMATION lsa_TRUST_INFORMATION = (Win32Native.LSA_TRUST_INFORMATION)Marshal.PtrToStructure(new IntPtr((long)lsa_REFERENCED_DOMAIN_LIST.Domains + (long)(j * Marshal.SizeOf(typeof(Win32Native.LSA_TRUST_INFORMATION)))), typeof(Win32Native.LSA_TRUST_INFORMATION));
							array3[j] = new SecurityIdentifier(lsa_TRUST_INFORMATION.Sid, true);
						}
						Win32Native.LSA_TRANSLATED_SID[] array4 = new Win32Native.LSA_TRANSLATED_SID[sourceAccounts.Count];
						invalidHandle2.ReadArray<Win32Native.LSA_TRANSLATED_SID>(0UL, array4, 0, array4.Length);
						int k = 0;
						while (k < sourceAccounts.Count)
						{
							Win32Native.LSA_TRANSLATED_SID lsa_TRANSLATED_SID2 = array4[k];
							switch (lsa_TRANSLATED_SID2.Use)
							{
							case 1:
							case 2:
							case 4:
							case 5:
							case 9:
								identityReferenceCollection.Add(new SecurityIdentifier(array3[lsa_TRANSLATED_SID2.DomainIndex], lsa_TRANSLATED_SID2.Rid));
								break;
							case 3:
							case 6:
							case 7:
							case 8:
								goto IL_3C8;
							default:
								goto IL_3C8;
							}
							IL_3DA:
							k++;
							continue;
							IL_3C8:
							someFailed = true;
							identityReferenceCollection.Add(sourceAccounts[k]);
							goto IL_3DA;
						}
					}
				}
				else
				{
					for (int l = 0; l < sourceAccounts.Count; l++)
					{
						identityReferenceCollection.Add(sourceAccounts[l]);
					}
				}
				result = identityReferenceCollection;
			}
			finally
			{
				safeLsaPolicyHandle.Dispose();
				invalidHandle.Dispose();
				invalidHandle2.Dispose();
			}
			return result;
		}

		// Token: 0x04000FD8 RID: 4056
		private readonly string _Name;

		// Token: 0x04000FD9 RID: 4057
		internal const int MaximumAccountNameLength = 256;

		// Token: 0x04000FDA RID: 4058
		internal const int MaximumDomainNameLength = 255;
	}
}
