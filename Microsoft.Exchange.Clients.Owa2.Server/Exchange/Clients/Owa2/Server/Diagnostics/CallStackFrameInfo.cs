using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x0200042D RID: 1069
	internal class CallStackFrameInfo
	{
		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x00083780 File Offset: 0x00081980
		// (set) Token: 0x06002476 RID: 9334 RVA: 0x00083788 File Offset: 0x00081988
		public string FunctionName { get; set; }

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x00083791 File Offset: 0x00081991
		// (set) Token: 0x06002478 RID: 9336 RVA: 0x00083799 File Offset: 0x00081999
		public string PackageName { get; set; }

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x000837A2 File Offset: 0x000819A2
		// (set) Token: 0x0600247A RID: 9338 RVA: 0x000837AA File Offset: 0x000819AA
		public int StartLine { get; set; }

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x0600247B RID: 9339 RVA: 0x000837B3 File Offset: 0x000819B3
		// (set) Token: 0x0600247C RID: 9340 RVA: 0x000837BB File Offset: 0x000819BB
		public int StartColumn { get; set; }

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x000837C4 File Offset: 0x000819C4
		// (set) Token: 0x0600247E RID: 9342 RVA: 0x000837CC File Offset: 0x000819CC
		public int EndLine { get; set; }

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x000837D5 File Offset: 0x000819D5
		// (set) Token: 0x06002480 RID: 9344 RVA: 0x000837DD File Offset: 0x000819DD
		public int EndColumn { get; set; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x000837E6 File Offset: 0x000819E6
		// (set) Token: 0x06002482 RID: 9346 RVA: 0x000837EE File Offset: 0x000819EE
		public string FileName { get; set; }

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x000837F7 File Offset: 0x000819F7
		// (set) Token: 0x06002484 RID: 9348 RVA: 0x000837FF File Offset: 0x000819FF
		public string FolderPath { get; set; }

		// Token: 0x06002485 RID: 9349 RVA: 0x00083808 File Offset: 0x00081A08
		public CallStackFrameInfo()
		{
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x00083810 File Offset: 0x00081A10
		public CallStackFrameInfo(bool browserGeneratedData)
		{
			this.browserGeneratedData = browserGeneratedData;
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x00083820 File Offset: 0x00081A20
		public string SanitizedFunctionName
		{
			get
			{
				if (this.sanitizedFunctionName == null)
				{
					string originalFunctionName = this.browserGeneratedData ? CallStackFrameInfo.NormalizeFunctionNameFromBrowser(this.FunctionName) : this.FunctionName;
					this.sanitizedFunctionName = this.SanitizeFunctionName(originalFunctionName);
				}
				return this.sanitizedFunctionName;
			}
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x00083864 File Offset: 0x00081A64
		public override string ToString()
		{
			return string.Format("\r\n   at {0}() in {1}{2} : line {3}", new object[]
			{
				this.SanitizedFunctionName,
				this.FolderPath,
				this.FileName,
				this.StartLine
			});
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000838AC File Offset: 0x00081AAC
		public string ToDetailedString()
		{
			return string.Format("\r\n   at {0}() in {1}{2} {3}:{4} to {5}:{6}", new object[]
			{
				this.FunctionName,
				this.FolderPath,
				this.FileName,
				this.StartLine,
				this.StartColumn,
				this.EndLine,
				this.EndColumn
			});
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x0008391E File Offset: 0x00081B1E
		public void UpdateHash(ref int hash)
		{
			hash = WatsonReport.ComputeHash(this.SanitizedFunctionName, hash);
			hash = WatsonReport.ComputeHash(this.FileName, hash);
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x00083940 File Offset: 0x00081B40
		private static string NormalizeFunctionNameFromBrowser(string originalFunctionName)
		{
			if (string.IsNullOrEmpty(originalFunctionName) || originalFunctionName[0] == '$' || originalFunctionName.Equals("Anonymous function"))
			{
				return "Anonymous";
			}
			if (originalFunctionName[0] == '_' && originalFunctionName.LastIndexOf(".$", StringComparison.InvariantCultureIgnoreCase) >= 0)
			{
				return "Anonymous";
			}
			return originalFunctionName;
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x00083994 File Offset: 0x00081B94
		private string SanitizeFunctionName(string originalFunctionName)
		{
			StringBuilder stringBuilder = new StringBuilder(originalFunctionName.Length);
			bool flag = false;
			foreach (char c in originalFunctionName)
			{
				char c2 = c;
				if (c2 != ' ')
				{
					switch (c2)
					{
					case '(':
					case ')':
						break;
					default:
						if (c2 != '.')
						{
							stringBuilder.Append(c);
						}
						else
						{
							flag = true;
							stringBuilder.Append(c);
						}
						break;
					}
				}
				else
				{
					stringBuilder.Append('_');
				}
			}
			if (!flag)
			{
				return string.Format("{0}.{1}", Path.GetFileNameWithoutExtension(this.FileName), stringBuilder);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040013EA RID: 5098
		private const string NormalizedAnonymousFunctionName = "Anonymous";

		// Token: 0x040013EB RID: 5099
		private const string AnonymousFunctionNameOnIE = "Anonymous function";

		// Token: 0x040013EC RID: 5100
		private const string NormalizedStackFrameFormat = "\r\n   at {0}() in {1}{2} : line {3}";

		// Token: 0x040013ED RID: 5101
		private const string NormalizedDetailedStackFrameFormat = "\r\n   at {0}() in {1}{2} {3}:{4} to {5}:{6}";

		// Token: 0x040013EE RID: 5102
		private readonly bool browserGeneratedData;

		// Token: 0x040013EF RID: 5103
		private string sanitizedFunctionName;
	}
}
