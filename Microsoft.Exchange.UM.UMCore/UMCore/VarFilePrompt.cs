using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000236 RID: 566
	internal class VarFilePrompt : VariablePrompt<string>
	{
		// Token: 0x06001098 RID: 4248 RVA: 0x0004A904 File Offset: 0x00048B04
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"wave",
				this.filename,
				this.filename,
				this.filename
			});
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0004A94B File Offset: 0x00048B4B
		internal override string ToSsml()
		{
			return FilePrompt.BuildAudioSsml(this.filename, base.ProsodyRate);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0004A960 File Offset: 0x00048B60
		protected override void InternalInitialize()
		{
			if (base.InitVal == null)
			{
				return;
			}
			string path;
			if (Path.IsPathRooted(base.InitVal))
			{
				path = base.InitVal;
			}
			else
			{
				path = Path.Combine(Util.WavPathFromCulture(base.Culture), base.InitVal);
			}
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(Strings.FileNotFound(path));
			}
			this.filename = path;
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "VarFilePrompt successfully intialized with filename {0}.", new object[]
			{
				this.filename
			});
		}

		// Token: 0x04000B97 RID: 2967
		private string filename;
	}
}
