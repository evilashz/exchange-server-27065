using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x02000331 RID: 817
	[ComVisible(true)]
	[Serializable]
	public class CodeConnectAccess
	{
		// Token: 0x06002968 RID: 10600 RVA: 0x000987B5 File Offset: 0x000969B5
		public CodeConnectAccess(string allowScheme, int allowPort)
		{
			if (!CodeConnectAccess.IsValidScheme(allowScheme))
			{
				throw new ArgumentOutOfRangeException("allowScheme");
			}
			this.SetCodeConnectAccess(allowScheme.ToLower(CultureInfo.InvariantCulture), allowPort);
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000987E4 File Offset: 0x000969E4
		public static CodeConnectAccess CreateOriginSchemeAccess(int allowPort)
		{
			CodeConnectAccess codeConnectAccess = new CodeConnectAccess();
			codeConnectAccess.SetCodeConnectAccess(CodeConnectAccess.OriginScheme, allowPort);
			return codeConnectAccess;
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x00098804 File Offset: 0x00096A04
		public static CodeConnectAccess CreateAnySchemeAccess(int allowPort)
		{
			CodeConnectAccess codeConnectAccess = new CodeConnectAccess();
			codeConnectAccess.SetCodeConnectAccess(CodeConnectAccess.AnyScheme, allowPort);
			return codeConnectAccess;
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x00098824 File Offset: 0x00096A24
		private CodeConnectAccess()
		{
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x0009882C File Offset: 0x00096A2C
		private void SetCodeConnectAccess(string lowerCaseScheme, int allowPort)
		{
			this._LowerCaseScheme = lowerCaseScheme;
			if (allowPort == CodeConnectAccess.DefaultPort)
			{
				this._LowerCasePort = "$default";
			}
			else if (allowPort == CodeConnectAccess.OriginPort)
			{
				this._LowerCasePort = "$origin";
			}
			else
			{
				if (allowPort < 0 || allowPort > 65535)
				{
					throw new ArgumentOutOfRangeException("allowPort");
				}
				this._LowerCasePort = allowPort.ToString(CultureInfo.InvariantCulture);
			}
			this._IntPort = allowPort;
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x0009889A File Offset: 0x00096A9A
		public string Scheme
		{
			get
			{
				return this._LowerCaseScheme;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x000988A2 File Offset: 0x00096AA2
		public int Port
		{
			get
			{
				return this._IntPort;
			}
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000988AC File Offset: 0x00096AAC
		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			CodeConnectAccess codeConnectAccess = o as CodeConnectAccess;
			return codeConnectAccess != null && this.Scheme == codeConnectAccess.Scheme && this.Port == codeConnectAccess.Port;
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000988F0 File Offset: 0x00096AF0
		public override int GetHashCode()
		{
			return this.Scheme.GetHashCode() + this.Port.GetHashCode();
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x00098918 File Offset: 0x00096B18
		internal CodeConnectAccess(string allowScheme, string allowPort)
		{
			if (allowScheme == null || allowScheme.Length == 0)
			{
				throw new ArgumentNullException("allowScheme");
			}
			if (allowPort == null || allowPort.Length == 0)
			{
				throw new ArgumentNullException("allowPort");
			}
			this._LowerCaseScheme = allowScheme.ToLower(CultureInfo.InvariantCulture);
			if (this._LowerCaseScheme == CodeConnectAccess.OriginScheme)
			{
				this._LowerCaseScheme = CodeConnectAccess.OriginScheme;
			}
			else if (this._LowerCaseScheme == CodeConnectAccess.AnyScheme)
			{
				this._LowerCaseScheme = CodeConnectAccess.AnyScheme;
			}
			else if (!CodeConnectAccess.IsValidScheme(this._LowerCaseScheme))
			{
				throw new ArgumentOutOfRangeException("allowScheme");
			}
			this._LowerCasePort = allowPort.ToLower(CultureInfo.InvariantCulture);
			if (this._LowerCasePort == "$default")
			{
				this._IntPort = CodeConnectAccess.DefaultPort;
				return;
			}
			if (this._LowerCasePort == "$origin")
			{
				this._IntPort = CodeConnectAccess.OriginPort;
				return;
			}
			this._IntPort = int.Parse(allowPort, CultureInfo.InvariantCulture);
			if (this._IntPort < 0 || this._IntPort > 65535)
			{
				throw new ArgumentOutOfRangeException("allowPort");
			}
			this._LowerCasePort = this._IntPort.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06002972 RID: 10610 RVA: 0x00098A53 File Offset: 0x00096C53
		internal bool IsOriginScheme
		{
			get
			{
				return this._LowerCaseScheme == CodeConnectAccess.OriginScheme;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x00098A62 File Offset: 0x00096C62
		internal bool IsAnyScheme
		{
			get
			{
				return this._LowerCaseScheme == CodeConnectAccess.AnyScheme;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06002974 RID: 10612 RVA: 0x00098A71 File Offset: 0x00096C71
		internal bool IsDefaultPort
		{
			get
			{
				return this.Port == CodeConnectAccess.DefaultPort;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06002975 RID: 10613 RVA: 0x00098A80 File Offset: 0x00096C80
		internal bool IsOriginPort
		{
			get
			{
				return this.Port == CodeConnectAccess.OriginPort;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06002976 RID: 10614 RVA: 0x00098A8F File Offset: 0x00096C8F
		internal string StrPort
		{
			get
			{
				return this._LowerCasePort;
			}
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x00098A98 File Offset: 0x00096C98
		internal static bool IsValidScheme(string scheme)
		{
			if (scheme == null || scheme.Length == 0 || !CodeConnectAccess.IsAsciiLetter(scheme[0]))
			{
				return false;
			}
			for (int i = scheme.Length - 1; i > 0; i--)
			{
				if (!CodeConnectAccess.IsAsciiLetterOrDigit(scheme[i]) && scheme[i] != '+' && scheme[i] != '-' && scheme[i] != '.')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x00098B05 File Offset: 0x00096D05
		private static bool IsAsciiLetterOrDigit(char character)
		{
			return CodeConnectAccess.IsAsciiLetter(character) || (character >= '0' && character <= '9');
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x00098B20 File Offset: 0x00096D20
		private static bool IsAsciiLetter(char character)
		{
			return (character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z');
		}

		// Token: 0x04001092 RID: 4242
		private string _LowerCaseScheme;

		// Token: 0x04001093 RID: 4243
		private string _LowerCasePort;

		// Token: 0x04001094 RID: 4244
		private int _IntPort;

		// Token: 0x04001095 RID: 4245
		private const string DefaultStr = "$default";

		// Token: 0x04001096 RID: 4246
		private const string OriginStr = "$origin";

		// Token: 0x04001097 RID: 4247
		internal const int NoPort = -1;

		// Token: 0x04001098 RID: 4248
		internal const int AnyPort = -2;

		// Token: 0x04001099 RID: 4249
		public static readonly int DefaultPort = -3;

		// Token: 0x0400109A RID: 4250
		public static readonly int OriginPort = -4;

		// Token: 0x0400109B RID: 4251
		public static readonly string OriginScheme = "$origin";

		// Token: 0x0400109C RID: 4252
		public static readonly string AnyScheme = "*";
	}
}
