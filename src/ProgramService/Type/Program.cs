using System;

namespace ProgramService.Type
{
	/// <summary>
    /// Outgoing Model for ProgramEntities
    /// </summary>
	public class Program
	{
		public string Name { get; set; } = string.Empty;
		public string Homepage { get; set; } = string.Empty;
		public IList<Version> Version { get; set; } = new List<Version>();
	}
}

