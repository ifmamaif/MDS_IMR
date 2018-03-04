//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include<fstream.h>
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;

TImage *b[50][50];
int lin, col; //memoreaza linia si coloana la mouseclic
int d=20; //latura patratelor imagine
int n,a[50][50]; //matricea de intrare - labirintul: 0=perete,1=spatiu
int l1,c1;  //coordonatele caracterului
//int l,c;
int lu,cu;//coordonatele usii
int clic;
// 2.bmp - usa, 3.bmp - cheie
int lk1,ck1,lk2,ck2;//coordonatele cheilor
int x1,y1,x2,y2,x3,y3,x4,y4,x5,x6,y5,y6; //delimiteaza zona caracterului pasare
int x,y,xa,ya,xa1,ya1; //coordonate pasare
int dir=1,dir1=2,dir2=3;//1 catre dreapta, -1 catre stanga, 2 catre jos, -2 catre sus
int L=10,T=10;

//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
        : TForm(Owner)
{
}
//---------------------------------------------------------------------------
//CITIREA DATELOR DIN FISIER
//--------------------------------------------------------------
void citire()
{fstream f;
int j,i;
f.open("matrice1.txt",ios::in);
f>>n;
for(i=1;i<=n;i++)
   for(j=1;j<=n;j++)
      f>>a[i][j];
f>>l1>>c1; //coordonate personaj
f>>lu>>cu; //coordonate usa
f>>lk1>>ck1;//coordonate cheia 1
f>>lk2>>ck2; //coordonate cheia 2
f>>x1>>y1>>x2>>y2;//interval inamic1(bird)
x=x1;y=y1;
f>>x3>>y3>>x4>>y4;//interval inamic2(bird)
xa=x3;ya=y3;
f>>x5>>y5>>x6>>y6;//interval inamic2(bird)
xa1=x5;ya1=y5;
}
//----------------------------------------------------------------------
//GENEREZ LABIRINTUL  - MATRICEA DE IMAGINI  b
//------------------------------- ---------------------------------------
 void generare_labirint()
 { int i,j;
   for(i=1;i<=n;i++)
       for(j=1;j<=n;j++)
          { b[i][j]=new TImage(Form1);
            b[i][j]->Width=d;
            b[i][j]->Height=d;
            b[i][j]->Stretch=true;
            if(i==1)
               b[i][j]->Top=T;
            else
               b[i][j]->Top=b[i-1][j]->Top+d;
            if(j==1)
               b[i][j]->Left=L;
            else
               b[i][j]->Left=b[i][j-1]->Left+d;   
            if(a[i][j]==0)
               b[i][j]->Picture->LoadFromFile("imagini//0.bmp");
            else
                b[i][j]->Picture->LoadFromFile("imagini//1.bmp");
           Form1->InsertControl(b[i][j]);
           b[i][j]->OnClick=Form1->Image1->OnClick;
          }

   b[l1][c1]->Picture->LoadFromFile("imagini//bd.bmp"); //caracter1: baiat
   b[lu][cu]->Picture->LoadFromFile("imagini//2.bmp"); //usa
   b[lk1][ck1]->Picture->LoadFromFile("imagini//3.bmp"); //cheie
   b[lk2][ck2]->Picture->LoadFromFile("imagini//3.bmp"); //cheie
   b[x][y]->Picture->LoadFromFile("imagini//4.bmp"); //pasare
   for(i=4;i<=7;i++)
   {j=25;
    b[i][j]->Picture->LoadFromFile("imagini//0.bmp");}
   for(j=26;j<=n;j++)
   {i=7;
   b[i][j]->Picture->LoadFromFile("imagini//0.bmp");}
   for(i=8;i<=n;i++)
   {j=30;
   b[i][j]->Picture->LoadFromFile("imagini//0.bmp");}
   for(j=29;j>19;j--)
   {i=30;
   b[i][j]->Picture->LoadFromFile("imagini//0.bmp");}
 }
//-------------------------------------------------------------------
void __fastcall TForm1::FormActivate(TObject *Sender)
{Form1->Top=5;
Form1->Left=5;
citire();
//stabilire dimensiuni formular
Form1->Width=n*d+60+d;
Form1->Height=n*d+100;
Label1->Top=n*d+30;
generare_labirint();

Image1->Left=b[n][n]->Left+d+3;
Image1->Top=b[n][n]->Top;
Image1->Width=d;
Image1->Height=d;
Image1->Stretch=true;
Label1->Left=10;
Label1->Top=b[n][n]->Top+d+3;    
}
//---------------------------------------------------------------------------
void __fastcall TForm1::FormKeyDown(TObject *Sender, WORD &Key,
      TShiftState Shift)
{int i,j;
if (Key==VK_UP)//tasta UP (sageata sus)
       if(l1>1 && a[l1-1][c1]!=0)
             {
              b[l1][c1]->Picture->LoadFromFile("imagini//1.bmp");
              l1--;
              b[l1][c1]->Picture->LoadFromFile("imagini//bu.bmp");
             }
if(Key==VK_DOWN) //tasta DOWN
      if(l1<n && a[l1+1][c1]!=0)
             { b[l1][c1]->Picture->LoadFromFile("imagini//1.bmp");
             l1++;
             b[l1][c1]->Picture->LoadFromFile("imagini//bd.bmp");
             }
if(Key==VK_LEFT)
      if(c1>1 && a[l1][c1-1]!=0)
             { b[l1][c1]->Picture->LoadFromFile("imagini//1.bmp");
             c1--;
             b[l1][c1]->Picture->LoadFromFile("imagini//bl.bmp");
             }
if(Key==VK_RIGHT)
      if(c1<n && a[l1][c1+1]!=0)
             { b[l1][c1]->Picture->LoadFromFile("imagini//1.bmp");
               c1++;
             b[l1][c1]->Picture->LoadFromFile("imagini//br.bmp");
             }
if(b[l1][c1]==b[x][y])
 {b[l1][c1]->Picture->LoadFromFile("imagini//4.bmp");
 l1=6; c1=1;
  b[l1][c1]->Picture->LoadFromFile("imagini//bd.bmp");
  ShowMessage("Restart the game");}
if(b[l1][c1]==b[30][19])
{ShowMessage("You win");
 l1=6; c1=1;
b[l1][c1]->Picture->LoadFromFile("imagini//bd.bmp");
b[30][19]->Picture->LoadFromFile("imagini//2.bmp");
   for(i=4;i<=7;i++)
   {j=25;
    b[i][j]->Picture->LoadFromFile("imagini//0.bmp");}
   for(j=26;j<=n;j++)
   {i=7;
   b[i][j]->Picture->LoadFromFile("imagini//0.bmp");}
   for(i=8;i<=n;i++)
   {j=30;
   b[i][j]->Picture->LoadFromFile("imagini//0.bmp");}
   for(j=29;j>19;j--)
   {i=30;
   b[i][j]->Picture->LoadFromFile("imagini//0.bmp");}}
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Timer1Timer(TObject *Sender)
{  //programare caracter pasare------
if(dir==1 && y<y2)
   {
    b[x][y]->Picture->LoadFromFile("imagini//1.bmp");
    y++;
    b[x][y]->Picture->LoadFromFile("imagini//4.bmp");
    }
else
   if(dir==1 && y==y2)
    {b[x][y]->Picture->LoadFromFile("imagini//1.bmp");
    y--;
    b[x][y]->Picture->LoadFromFile("imagini//4.bmp");
    dir=-1;
    }
 else
   if(dir==-1 && y>y1)
    {b[x][y]->Picture->LoadFromFile("imagini//1.bmp");
     y--;
     b[x][y]->Picture->LoadFromFile("imagini//4.bmp");
    }
 else
   if(dir==-1 && y==y1)
   { b[x][y]->Picture->LoadFromFile("imagini//1.bmp");
     y++;
     b[x][y]->Picture->LoadFromFile("imagini//4.bmp");
     dir=1;
    }
 if(b[l1][c1]==b[x][y])
 {b[l1][c1]->Picture->LoadFromFile("imagini//4.bmp");
 l1=6; c1=1;
 b[l1][c1]->Picture->LoadFromFile("imagini//bd.bmp");
ShowMessage("Restart the game");}
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Timer2Timer(TObject *Sender)
{  //programare caracter pasare------
if(dir1==2 && xa<x4)
   {
    b[xa][y3]->Picture->LoadFromFile("imagini//1.bmp");
    xa++;
    b[xa][y3]->Picture->LoadFromFile("imagini//4.bmp");
    }
else
if(dir1==2 && xa==x4)
    {b[xa][y3]->Picture->LoadFromFile("imagini//1.bmp");
    xa--;
    b[xa][y3]->Picture->LoadFromFile("imagini//4.bmp");
    dir1=-2;
    }
else
if(dir1==-2 && xa>x3)
    {b[xa][y3]->Picture->LoadFromFile("imagini//1.bmp");
     xa--;
     b[xa][y3]->Picture->LoadFromFile("imagini//4.bmp");
    }
else
if(dir1==-2 && xa==x3)
   { b[xa][y3]->Picture->LoadFromFile("imagini//1.bmp");
     xa++;
     b[xa][y3]->Picture->LoadFromFile("imagini//4.bmp");
     dir1=2;
    }
 if(b[l1][c1]==b[xa][y3])
 {b[l1][c1]->Picture->LoadFromFile("imagini//4.bmp");
 l1=6; c1=1;
 b[l1][c1]->Picture->LoadFromFile("imagini//bd.bmp");
ShowMessage("Restart the game");}
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Timer3Timer(TObject *Sender)
{  //programare caracter pasare
if(dir2==3 && xa1<x6)
   {
    b[xa1][y5]->Picture->LoadFromFile("imagini//1.bmp");
    xa1++;
    b[xa1][y5]->Picture->LoadFromFile("imagini//4.bmp");
    }
else
if(dir2==3 && xa1==x6)
    {b[xa1][y5]->Picture->LoadFromFile("imagini//1.bmp");
    xa1--;
    b[xa1][y5]->Picture->LoadFromFile("imagini//4.bmp");
    dir2=-3;
    }
else
if(dir2==-3 && xa1>x5)
    {b[xa1][y5]->Picture->LoadFromFile("imagini//1.bmp");
     xa1--;
     b[xa1][y5]->Picture->LoadFromFile("imagini//4.bmp");
    }
else
if(dir2==-3 && xa1==x5)
   { b[xa1][y5]->Picture->LoadFromFile("imagini//1.bmp");
     xa1++;
     b[xa1][y5]->Picture->LoadFromFile("imagini//4.bmp");
     dir2=3;
    }
 if(b[l1][c1]==b[xa1][y5])
 {b[l1][c1]->Picture->LoadFromFile("imagini//4.bmp");
 l1=6; c1=1;
 b[l1][c1]->Picture->LoadFromFile("imagini//bd.bmp");
ShowMessage("Restart the game");}
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Image1Click(TObject *Sender)
{int i,j;
for(i=1;i<=n;i++)
    for(j=1;j<=n;j++)
        if(b[i][j]==Sender)
             {lin=i;
             col=j;}

  Label1->Caption=IntToStr(lin)+" "+IntToStr(col);
}
//---------------------------------------------------------------------------

