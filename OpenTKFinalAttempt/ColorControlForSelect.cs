using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenTKFinalAttempt
{
    public partial class ColorControlForSelect : UserControl
    {
        public delegate void eventForColorChanged(Color in_Color, int index);
        /// <summary>
        /// contains labels which are shown
        /// </summary>
        private List<Label> colorList = new List<Label>();
        private int theBestHeightForRow = 20;
        private int currentNumerOfRows = 1;
        public event eventForColorChanged eventForColorChangedHandler;
        /// <summary>
        /// get size of entries
        /// </summary>
        /// <returns></returns>
        public int getListLength()
        {
            return colorList.Count;
        }
        /// <summary>
        /// adds new color to list
        /// </summary>
        /// <param name="in_Color">a color to insert</param>
        /// <param name="indexToInsert">index where to insert</param>
        public void insertColor(Color in_Color, int indexToInsert=0)  {
            if (indexToInsert > colorList.Count) {
                throw new IndexOutOfRangeException();
            }
            //new row
            this.tableLayoutPanelContainer.RowCount += 1;
            currentNumerOfRows += 1;
            //shift all controls
            for (int i=indexToInsert; i<colorList.Count; i++)     {
                tableLayoutPanelContainer.SetRow(colorList[i], i + 1);
            }
            tableLayoutPanelContainer.RowStyles[indexToInsert].Height = theBestHeightForRow;
            Label nLabel = new Label();
            nLabel.Dock = DockStyle.Fill;
            nLabel.BackColor = in_Color;
            colorList.Insert(indexToInsert, nLabel);
            tableLayoutPanelContainer.SetRow(colorList[indexToInsert], indexToInsert);
        }
        /// <summary>
        /// actual working routine for setting up colors
        /// </summary>
        /// <param name="in_Colors"></param>
        public void insertAllColors(List<Color> in_Colors)
        {
            colorList.Clear();
            tableLayoutPanelContainer.RowCount = 1;
            int currentRow = 0;
            foreach (Color itemColor in in_Colors)  {
                colorList.Add(new Label());
                colorList[colorList.Count - 1].Dock = DockStyle.Fill;
                colorList[colorList.Count - 1].Name = "Label" + currentRow.ToString();
                colorList[colorList.Count - 1].BackColor = itemColor;
                colorList[colorList.Count - 1].Click += label_Click;
                tableLayoutPanelContainer.RowCount += 1;
                tableLayoutPanelContainer.RowStyles.Add(new RowStyle(SizeType.Absolute));
                tableLayoutPanelContainer.RowStyles[currentRow].Height = theBestHeightForRow;
                tableLayoutPanelContainer.Controls.Add(colorList[colorList.Count - 1], 0, currentRow);
                currentRow++;
            }
        }
        private void label_Click(object sender, System.EventArgs e)
        {
            ColorDialog colorSelector = new ColorDialog();
            colorSelector.Color = (sender as Label).BackColor;
            if (colorSelector.ShowDialog() != DialogResult.Cancel)
            {
                (sender as Label).BackColor = colorSelector.Color;
                eventForColorChangedHandler(colorSelector.Color,int.Parse( (sender as Label).Name.Substring(5) ));

            }
            
        }
        public ColorControlForSelect()
        {
            InitializeComponent();
        }
    }
}
