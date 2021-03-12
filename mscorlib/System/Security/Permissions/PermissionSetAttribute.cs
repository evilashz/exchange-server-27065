using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Security.Util;
using System.Text;

namespace System.Security.Permissions
{
	// Token: 0x020002D4 RID: 724
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class PermissionSetAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060025DC RID: 9692 RVA: 0x000885B2 File Offset: 0x000867B2
		public PermissionSetAttribute(SecurityAction action) : base(action)
		{
			this.m_unicode = false;
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x000885C2 File Offset: 0x000867C2
		// (set) Token: 0x060025DE RID: 9694 RVA: 0x000885CA File Offset: 0x000867CA
		public string File
		{
			get
			{
				return this.m_file;
			}
			set
			{
				this.m_file = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060025DF RID: 9695 RVA: 0x000885D3 File Offset: 0x000867D3
		// (set) Token: 0x060025E0 RID: 9696 RVA: 0x000885DB File Offset: 0x000867DB
		public bool UnicodeEncoded
		{
			get
			{
				return this.m_unicode;
			}
			set
			{
				this.m_unicode = value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060025E1 RID: 9697 RVA: 0x000885E4 File Offset: 0x000867E4
		// (set) Token: 0x060025E2 RID: 9698 RVA: 0x000885EC File Offset: 0x000867EC
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x000885F5 File Offset: 0x000867F5
		// (set) Token: 0x060025E4 RID: 9700 RVA: 0x000885FD File Offset: 0x000867FD
		public string XML
		{
			get
			{
				return this.m_xml;
			}
			set
			{
				this.m_xml = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x00088606 File Offset: 0x00086806
		// (set) Token: 0x060025E6 RID: 9702 RVA: 0x0008860E File Offset: 0x0008680E
		public string Hex
		{
			get
			{
				return this.m_hex;
			}
			set
			{
				this.m_hex = value;
			}
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x00088617 File Offset: 0x00086817
		public override IPermission CreatePermission()
		{
			return null;
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x0008861C File Offset: 0x0008681C
		private PermissionSet BruteForceParseStream(Stream stream)
		{
			Encoding[] array = new Encoding[]
			{
				Encoding.UTF8,
				Encoding.ASCII,
				Encoding.Unicode
			};
			StreamReader streamReader = null;
			Exception ex = null;
			int num = 0;
			while (streamReader == null && num < array.Length)
			{
				try
				{
					stream.Position = 0L;
					streamReader = new StreamReader(stream, array[num]);
					return this.ParsePermissionSet(new Parser(streamReader));
				}
				catch (Exception ex2)
				{
					if (ex == null)
					{
						ex = ex2;
					}
				}
				num++;
			}
			throw ex;
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x000886A0 File Offset: 0x000868A0
		private PermissionSet ParsePermissionSet(Parser parser)
		{
			SecurityElement topElement = parser.GetTopElement();
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.FromXml(topElement);
			return permissionSet;
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000886C4 File Offset: 0x000868C4
		[SecuritySafeCritical]
		public PermissionSet CreatePermissionSet()
		{
			if (this.m_unrestricted)
			{
				return new PermissionSet(PermissionState.Unrestricted);
			}
			if (this.m_name != null)
			{
				return PolicyLevel.GetBuiltInSet(this.m_name);
			}
			if (this.m_xml != null)
			{
				return this.ParsePermissionSet(new Parser(this.m_xml.ToCharArray()));
			}
			if (this.m_hex != null)
			{
				return this.BruteForceParseStream(new MemoryStream(System.Security.Util.Hex.DecodeHexString(this.m_hex)));
			}
			if (this.m_file != null)
			{
				return this.BruteForceParseStream(new FileStream(this.m_file, FileMode.Open, FileAccess.Read));
			}
			return new PermissionSet(PermissionState.None);
		}

		// Token: 0x04000E55 RID: 3669
		private string m_file;

		// Token: 0x04000E56 RID: 3670
		private string m_name;

		// Token: 0x04000E57 RID: 3671
		private bool m_unicode;

		// Token: 0x04000E58 RID: 3672
		private string m_xml;

		// Token: 0x04000E59 RID: 3673
		private string m_hex;
	}
}
