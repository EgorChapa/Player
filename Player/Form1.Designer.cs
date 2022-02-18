using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Player
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Text = "Player";

            OpenForm();
            this.FormClosing += Form1_FormClosing;

            delete_music.Text = "Delete";
            delete_music.Location = new Point(300, 300);
            delete_music.Size = new System.Drawing.Size(50, 50);
            delete_music.Click += Delete_music_Click;
            Controls.Add(delete_music);

            play.Location = new Point(175, 150);
            play.Size = new System.Drawing.Size(50, 50);
            play.SizeMode = PictureBoxSizeMode.Zoom;
            play.Image = Image.FromFile("2088539.png");
            play.Click += Play_Click;
            Controls.Add(play);

            right.Location = new Point(225, 150);
            right.Text = ">";
            right.Size = new System.Drawing.Size(50, 50);
            right.Click += Right_Click;
            Controls.Add(right);

            left.Location = new Point(125, 150);
            left.Text = "<";
            left.Size = new System.Drawing.Size(50, 50);
            left.Click += Left_Click;
            Controls.Add(left);

            add_music.Location = new Point(300, 250);
            add_music.Text = "Add";
            add_music.Size = new System.Drawing.Size(50, 50);
            add_music.Click += Add_music_Click;
            Controls.Add(add_music);

            image.Location = new Point(125, 0);
            image.Size = new System.Drawing.Size(150, 150);
            image.SizeMode = PictureBoxSizeMode.Zoom;
            image.Image = Image.FromFile("CompactDisc.jpg");
            Controls.Add(image);

            Controls.Add(info);

            musics.Size = new System.Drawing.Size(250, 200);
            musics.Location = new Point(50, 250);
            Controls.Add(musics);
        }
        public int music_ = 0;
        bool x = true;
        string[] path;

        private void OpenForm()
        {
            if (File.Exists("info.txt"))
            {
                path = File.ReadAllLines("info.txt");
                for (int i = 0; i < path.Length; i++)
                {
                    file_ = new FileInfo(path[i]);
                    paths.Add(path[i]);
                    musics.Items.Add(file_.Name);
                }
                music_ = musics.Items.Count - 1;

            }
        }

        private void Right_Click(object sender, EventArgs e)
        {
            if (musics.SelectedIndex != -1)
            {
                stop();
                if (musics.SelectedIndex == music_)
                {
                    musics.SelectedIndex = 0;
                    start_();

                }
                else
                {
                    play.Image = Image.FromFile("images.png");
                    musics.SelectedIndex++;
                    start_();
                }
            }
        }

        private void Left_Click(object sender, EventArgs e)
        {
            if (musics.SelectedIndex != -1)
            {
                stop();
                if (musics.SelectedIndex == 0)
                {
                    musics.SelectedIndex = music_;
                    start_();

                }
                else
                {
                    play.Image = Image.FromFile("images.png");
                    musics.SelectedIndex--;
                    start_();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllLines("info.txt", paths);
        }

        private void Play_Click(object sender, System.EventArgs e)
        {
            if (x == true)
            {
                start_();
            }
            else
            {
                stop();
            }
        }

        private void stop()
        {
            outputDevice?.Stop();
            x = true;
            play.Image = Image.FromFile("2088539.png");
        }
        private void start_()
        {
            if (musics.SelectedIndex != -1)
            {
                outputDevice = new WaveOutEvent();
                audioFile = new AudioFileReader(paths[musics.SelectedIndex]);
                outputDevice.Init(audioFile);
                outputDevice.Play();
                play.Image = Image.FromFile("images.png");
                x = false;
                outputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;
            }
            else
            {
                MessageBox.Show("Выберите музыку");
            }
        }

        private void OutputDevice_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            stop();
        }

        private void Delete_music_Click(object sender, System.EventArgs e)
        {
            if (musics.SelectedIndex != -1)
            {
                stop();
                paths.RemoveAt(musics.SelectedIndex);
                music_--;
                musics.Items.RemoveAt(musics.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Выберите музыку");
            }
        }

        private void Add_music_Click(object sender, System.EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "*.mp3|*.mp3|*.wav|*.wav";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    music_++;
                    paths.Add(dialog.FileName);
                    musics.Items.Add(dialog.SafeFileName);

                }
            }
        }
        Button add_music = new Button();
        Button delete_music = new Button();
        PictureBox play = new PictureBox();
        Button right = new Button();
        Button left = new Button();

        Label info = new Label();

        PictureBox image = new PictureBox();

        List<string> paths = new List<string>();
        ListBox musics = new ListBox();

        AudioFileReader audioFile;
        WaveOutEvent outputDevice;

        FileInfo file_;
        #endregion
    }
}

