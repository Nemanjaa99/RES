using Common.Model;
using System.Collections.Generic;

namespace Common.FileUpload.Interfaces
{
	public interface IFileDialog
	{
		string GetDialogPathName();

		List<PowerRecord> Load(string path);

		void SaveToXML(OutputModel outputModel, string path);
	}
}