using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataBindingCollections_TRPO
{
    public class BudgetEstimateList
    {
        public List<BudgetEstimate> budgetEstimates = new List<BudgetEstimate>();

        void Save(string fileName)
        {
            string jsonString = JsonSerializer.Serialize(budgetEstimates);

            using (StreamWriter sw = File.CreateText(fileName))
            {
                sw.WriteLine(jsonString);
            }
        }

        void Load(string fileName)
        {
            using (StreamReader sr = File.OpenText(fileName))
            {
                string jsonString = sr.ReadToEnd();


                List<BudgetEstimate>? budgetEstimatesLoaded = JsonSerializer.Deserialize<List<BudgetEstimate>>(jsonString);

                if (budgetEstimatesLoaded.Count > 0 && budgetEstimatesLoaded != null)
                {
                    budgetEstimates.Clear();
                    for (int i = 0; i < budgetEstimatesLoaded.Count; i++)
                    {
                        budgetEstimates.Add(budgetEstimatesLoaded[i]);
                    }
                }
            }
        }
    }
}
