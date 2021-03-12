using System;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x020003C9 RID: 969
	[Serializable]
	internal class LogSwitch
	{
		// Token: 0x06003231 RID: 12849 RVA: 0x000C0767 File Offset: 0x000BE967
		private LogSwitch()
		{
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x000C0770 File Offset: 0x000BE970
		[SecuritySafeCritical]
		public LogSwitch(string name, string description, LogSwitch parent)
		{
			if (name != null && name.Length == 0)
			{
				throw new ArgumentOutOfRangeException("Name", Environment.GetResourceString("Argument_StringZeroLength"));
			}
			if (name != null && parent != null)
			{
				this.strName = name;
				this.strDescription = description;
				this.iLevel = LoggingLevels.ErrorLevel;
				this.iOldLevel = this.iLevel;
				this.ParentSwitch = parent;
				Log.m_Hashtable.Add(this.strName, this);
				Log.AddLogSwitch(this);
				return;
			}
			throw new ArgumentNullException((name == null) ? "name" : "parent");
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x000C0804 File Offset: 0x000BEA04
		[SecuritySafeCritical]
		internal LogSwitch(string name, string description)
		{
			this.strName = name;
			this.strDescription = description;
			this.iLevel = LoggingLevels.ErrorLevel;
			this.iOldLevel = this.iLevel;
			this.ParentSwitch = null;
			Log.m_Hashtable.Add(this.strName, this);
			Log.AddLogSwitch(this);
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x000C085D File Offset: 0x000BEA5D
		public virtual string Name
		{
			get
			{
				return this.strName;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x000C0865 File Offset: 0x000BEA65
		public virtual string Description
		{
			get
			{
				return this.strDescription;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x000C086D File Offset: 0x000BEA6D
		public virtual LogSwitch Parent
		{
			get
			{
				return this.ParentSwitch;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06003237 RID: 12855 RVA: 0x000C0875 File Offset: 0x000BEA75
		// (set) Token: 0x06003238 RID: 12856 RVA: 0x000C0880 File Offset: 0x000BEA80
		public virtual LoggingLevels MinimumLevel
		{
			get
			{
				return this.iLevel;
			}
			[SecuritySafeCritical]
			set
			{
				this.iLevel = value;
				this.iOldLevel = value;
				string strParentName = (this.ParentSwitch != null) ? this.ParentSwitch.Name : "";
				if (Debugger.IsAttached)
				{
					Log.ModifyLogSwitch((int)this.iLevel, this.strName, strParentName);
				}
				Log.InvokeLogSwitchLevelHandlers(this, this.iLevel);
			}
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x000C08E3 File Offset: 0x000BEAE3
		public virtual bool CheckLevel(LoggingLevels level)
		{
			return this.iLevel <= level || (this.ParentSwitch != null && this.ParentSwitch.CheckLevel(level));
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x000C0908 File Offset: 0x000BEB08
		public static LogSwitch GetSwitch(string name)
		{
			return (LogSwitch)Log.m_Hashtable[name];
		}

		// Token: 0x0400160E RID: 5646
		internal string strName;

		// Token: 0x0400160F RID: 5647
		internal string strDescription;

		// Token: 0x04001610 RID: 5648
		private LogSwitch ParentSwitch;

		// Token: 0x04001611 RID: 5649
		internal volatile LoggingLevels iLevel;

		// Token: 0x04001612 RID: 5650
		internal volatile LoggingLevels iOldLevel;
	}
}
