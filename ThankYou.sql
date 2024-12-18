PGDMP  3                
    |            ThankYou    16.2    16.4 4               0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    16989    ThankYou    DATABASE     ~   CREATE DATABASE "ThankYou" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "ThankYou";
                postgres    false            �            1255    17386    calculaterating()    FUNCTION       CREATE FUNCTION public.calculaterating() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
BEGIN 
	update employees
	set employee_rating = (select avg(grade) from employees, tips where employees.id = new.employee_id)
	where employees.id = new.employee_id;
	return new;
END; 
$$;
 (   DROP FUNCTION public.calculaterating();
       public          postgres    false            �            1255    17374    calculaterating(smallint)    FUNCTION       CREATE FUNCTION public.calculaterating(employeeid smallint) RETURNS void
    LANGUAGE sql
    AS $$
	update employees 
	set employee_rating = (select avg(grade) from tips, employees where employees.id = employeeId)
	where employees.id = employeeId;
$$;
 ;   DROP FUNCTION public.calculaterating(employeeid smallint);
       public          postgres    false            �            1255    17369    isexist(smallint)    FUNCTION     �   CREATE FUNCTION public.isexist(newid smallint) RETURNS boolean
    LANGUAGE sql
    AS $$
	select COUNT(*) = 1 from users where users.id = newId;
$$;
 .   DROP FUNCTION public.isexist(newid smallint);
       public          postgres    false            �            1259    17338 
   bank_cards    TABLE     �  CREATE TABLE public.bank_cards (
    card_number character varying NOT NULL,
    owner smallint NOT NULL,
    bank_number character varying NOT NULL,
    CONSTRAINT bank_cards_check_number_length CHECK ((length((card_number)::text) = 16)),
    CONSTRAINT bank_cards_check_only_numbers CHECK (((card_number)::text ~ similar_to_escape('[0-9]+'::text))),
    CONSTRAINT "bank_cards_check_owner_isExist" CHECK (public.isexist(owner))
);
    DROP TABLE public.bank_cards;
       public         heap    postgres    false    225                       0    0    TABLE bank_cards    ACL     0   GRANT ALL ON TABLE public.bank_cards TO "User";
          public          postgres    false    224            �            1259    17094    banks    TABLE     -  CREATE TABLE public.banks (
    bank_code character varying NOT NULL,
    bank_name character varying NOT NULL,
    CONSTRAINT banks_check_number_length CHECK ((length((bank_code)::text) = 9)),
    CONSTRAINT banks_check_only_numbers CHECK (((bank_code)::text ~ similar_to_escape('[0-9]+'::text)))
);
    DROP TABLE public.banks;
       public         heap    postgres    false                       0    0    TABLE banks    ACL     +   GRANT ALL ON TABLE public.banks TO "User";
          public          postgres    false    218            �            1259    17006    users    TABLE     .  CREATE TABLE public.users (
    id smallint NOT NULL,
    surname character varying NOT NULL,
    name character varying NOT NULL,
    patronymic character varying,
    phone_number character varying NOT NULL,
    password character varying NOT NULL,
    CONSTRAINT users_check_phone_first_symbol CHECK (((phone_number)::text ~~ '8%'::text)),
    CONSTRAINT users_check_phone_number_length CHECK ((length((phone_number)::text) = 11)),
    CONSTRAINT users_check_phone_number_only_numbers CHECK (((phone_number)::text ~ similar_to_escape('[0-9]+'::text)))
);
    DROP TABLE public.users;
       public         heap    postgres    false                       0    0    TABLE users    ACL     +   GRANT ALL ON TABLE public.users TO "User";
          public          postgres    false    216            �            1259    17254    clients    TABLE     9   CREATE TABLE public.clients (
)
INHERITS (public.users);
    DROP TABLE public.clients;
       public         heap    postgres    false    216            	           0    0    TABLE clients    ACL     -   GRANT ALL ON TABLE public.clients TO "User";
          public          postgres    false    223            �            1259    17171 	   employees    TABLE     �  CREATE TABLE public.employees (
    merchant_id smallint NOT NULL,
    employee_rating real NOT NULL,
    "position" character varying NOT NULL,
    CONSTRAINT employees_check_phone_number_first_symbol CHECK (((phone_number)::text ~~ '8%'::text)),
    CONSTRAINT employees_check_phone_number_length CHECK ((length((phone_number)::text) = 11)),
    CONSTRAINT employees_check_phone_number_only_number CHECK (((phone_number)::text ~ similar_to_escape('[0-9]+'::text))),
    CONSTRAINT employees_check_ratting_in_correct_range CHECK (((employee_rating >= (0)::double precision) AND (employee_rating <= (5)::double precision)))
)
INHERITS (public.users);
    DROP TABLE public.employees;
       public         heap    postgres    false    216            
           0    0    TABLE employees    ACL     /   GRANT ALL ON TABLE public.employees TO "User";
          public          postgres    false    221            �            1259    17019 	   merchants    TABLE     �   CREATE TABLE public.merchants (
    inn smallint NOT NULL,
    address character varying NOT NULL,
    menu character varying,
    password character varying NOT NULL,
    name character varying NOT NULL
);
    DROP TABLE public.merchants;
       public         heap    postgres    false                       0    0    TABLE merchants    ACL     /   GRANT ALL ON TABLE public.merchants TO "User";
          public          postgres    false    217            �            1259    17005    peoples_id_seq    SEQUENCE     �   ALTER TABLE public.users ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.peoples_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    216            �            1259    17225 	   positions    TABLE     G   CREATE TABLE public.positions (
    name character varying NOT NULL
);
    DROP TABLE public.positions;
       public         heap    postgres    false                       0    0    TABLE positions    ACL     /   GRANT ALL ON TABLE public.positions TO "User";
          public          postgres    false    222            �            1259    17104    tips    TABLE     6  CREATE TABLE public.tips (
    id integer NOT NULL,
    client_id smallint,
    employee_id smallint NOT NULL,
    sum smallint NOT NULL,
    grade smallint NOT NULL,
    review character varying,
    date date NOT NULL,
    CONSTRAINT tips_check_grade_correct_range CHECK (((grade >= 0) AND (grade <= 5)))
);
    DROP TABLE public.tips;
       public         heap    postgres    false                       0    0 
   TABLE tips    ACL     *   GRANT ALL ON TABLE public.tips TO "User";
          public          postgres    false    220            �            1259    17103    tips_id_seq    SEQUENCE     �   ALTER TABLE public.tips ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.tips_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    220            ;           2604    17413 
   clients id    DEFAULT     h   ALTER TABLE ONLY public.clients ALTER COLUMN id SET DEFAULT nextval('public.peoples_id_seq'::regclass);
 9   ALTER TABLE public.clients ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    215    223            :           2604    17414    employees id    DEFAULT     j   ALTER TABLE ONLY public.employees ALTER COLUMN id SET DEFAULT nextval('public.peoples_id_seq'::regclass);
 ;   ALTER TABLE public.employees ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    221    215            �          0    17338 
   bank_cards 
   TABLE DATA           E   COPY public.bank_cards (card_number, owner, bank_number) FROM stdin;
    public          postgres    false    224   �=       �          0    17094    banks 
   TABLE DATA           5   COPY public.banks (bank_code, bank_name) FROM stdin;
    public          postgres    false    218   �=       �          0    17254    clients 
   TABLE DATA           X   COPY public.clients (id, surname, name, patronymic, phone_number, password) FROM stdin;
    public          postgres    false    223   >       �          0    17171 	   employees 
   TABLE DATA           �   COPY public.employees (id, surname, name, patronymic, phone_number, merchant_id, employee_rating, password, "position") FROM stdin;
    public          postgres    false    221   V>       �          0    17019 	   merchants 
   TABLE DATA           G   COPY public.merchants (inn, address, menu, password, name) FROM stdin;
    public          postgres    false    217   �>       �          0    17225 	   positions 
   TABLE DATA           )   COPY public.positions (name) FROM stdin;
    public          postgres    false    222   7?       �          0    17104    tips 
   TABLE DATA           T   COPY public.tips (id, client_id, employee_id, sum, grade, review, date) FROM stdin;
    public          postgres    false    220   u?       �          0    17006    users 
   TABLE DATA           V   COPY public.users (id, surname, name, patronymic, phone_number, password) FROM stdin;
    public          postgres    false    216   �?                  0    0    peoples_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.peoples_id_seq', 19, true);
          public          postgres    false    215                       0    0    tips_id_seq    SEQUENCE SET     9   SELECT pg_catalog.setval('public.tips_id_seq', 4, true);
          public          postgres    false    219            `           2606    17344    bank_cards bank_cards_pk 
   CONSTRAINT     _   ALTER TABLE ONLY public.bank_cards
    ADD CONSTRAINT bank_cards_pk PRIMARY KEY (card_number);
 B   ALTER TABLE ONLY public.bank_cards DROP CONSTRAINT bank_cards_pk;
       public            postgres    false    224            T           2606    17351    banks banks_pk 
   CONSTRAINT     S   ALTER TABLE ONLY public.banks
    ADD CONSTRAINT banks_pk PRIMARY KEY (bank_code);
 8   ALTER TABLE ONLY public.banks DROP CONSTRAINT banks_pk;
       public            postgres    false    218            V           2606    17102    banks banks_unique 
   CONSTRAINT     R   ALTER TABLE ONLY public.banks
    ADD CONSTRAINT banks_unique UNIQUE (bank_name);
 <   ALTER TABLE ONLY public.banks DROP CONSTRAINT banks_unique;
       public            postgres    false    218            ^           2606    17263    clients clients_pk 
   CONSTRAINT     P   ALTER TABLE ONLY public.clients
    ADD CONSTRAINT clients_pk PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.clients DROP CONSTRAINT clients_pk;
       public            postgres    false    223            Z           2606    17183    employees employees_pk 
   CONSTRAINT     T   ALTER TABLE ONLY public.employees
    ADD CONSTRAINT employees_pk PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.employees DROP CONSTRAINT employees_pk;
       public            postgres    false    221            R           2606    17188    merchants merchants_pk 
   CONSTRAINT     U   ALTER TABLE ONLY public.merchants
    ADD CONSTRAINT merchants_pk PRIMARY KEY (inn);
 @   ALTER TABLE ONLY public.merchants DROP CONSTRAINT merchants_pk;
       public            postgres    false    217            \           2606    17389    positions positions_pk 
   CONSTRAINT     V   ALTER TABLE ONLY public.positions
    ADD CONSTRAINT positions_pk PRIMARY KEY (name);
 @   ALTER TABLE ONLY public.positions DROP CONSTRAINT positions_pk;
       public            postgres    false    222            X           2606    17110    tips tips_pk 
   CONSTRAINT     J   ALTER TABLE ONLY public.tips
    ADD CONSTRAINT tips_pk PRIMARY KEY (id);
 6   ALTER TABLE ONLY public.tips DROP CONSTRAINT tips_pk;
       public            postgres    false    220            P           2606    17061    users users_pk 
   CONSTRAINT     L   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pk PRIMARY KEY (id);
 8   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pk;
       public            postgres    false    216            f           2620    17387    tips updaterating    TRIGGER     z   CREATE TRIGGER updaterating AFTER INSERT OR UPDATE ON public.tips FOR EACH ROW EXECUTE FUNCTION public.calculaterating();
 *   DROP TRIGGER updaterating ON public.tips;
       public          postgres    false    227    220            e           2606    17359    bank_cards bank_cards_banks_fk    FK CONSTRAINT     �   ALTER TABLE ONLY public.bank_cards
    ADD CONSTRAINT bank_cards_banks_fk FOREIGN KEY (bank_number) REFERENCES public.banks(bank_code);
 H   ALTER TABLE ONLY public.bank_cards DROP CONSTRAINT bank_cards_banks_fk;
       public          postgres    false    218    4692    224            c           2606    17189     employees employees_merchants_fk    FK CONSTRAINT     �   ALTER TABLE ONLY public.employees
    ADD CONSTRAINT employees_merchants_fk FOREIGN KEY (merchant_id) REFERENCES public.merchants(inn);
 J   ALTER TABLE ONLY public.employees DROP CONSTRAINT employees_merchants_fk;
       public          postgres    false    4690    217    221            d           2606    17401     employees employees_positions_fk    FK CONSTRAINT     �   ALTER TABLE ONLY public.employees
    ADD CONSTRAINT employees_positions_fk FOREIGN KEY ("position") REFERENCES public.positions(name);
 J   ALTER TABLE ONLY public.employees DROP CONSTRAINT employees_positions_fk;
       public          postgres    false    221    4700    222            a           2606    17264    tips tips_clients_fk    FK CONSTRAINT     w   ALTER TABLE ONLY public.tips
    ADD CONSTRAINT tips_clients_fk FOREIGN KEY (client_id) REFERENCES public.clients(id);
 >   ALTER TABLE ONLY public.tips DROP CONSTRAINT tips_clients_fk;
       public          postgres    false    223    4702    220            b           2606    17248    tips tips_employees_fk    FK CONSTRAINT     }   ALTER TABLE ONLY public.tips
    ADD CONSTRAINT tips_employees_fk FOREIGN KEY (employee_id) REFERENCES public.employees(id);
 @   ALTER TABLE ONLY public.tips DROP CONSTRAINT tips_employees_fk;
       public          postgres    false    220    221    4698            �      x�34D��f�&�f��&�\1z\\\ t�$      �   .   x�# ��845678945	Альфа-банк
\.


      �   ,   x�34�,,�,�,O崰45�047434�4JM),O������� ��|      �   o   x����0CϙbP�'$�l{�8��5pAb?��t�A#l��ArO���s�O�h���-��}|�{��<�`�:K����I�-"T>��ElZ��E.z��/�Gaf'�m3�      �   R   x�3�46�,OI-/�,O-*�,���0���;.����.̿����=ہ��.�P����F��m�^�أ����� ��)�      �   .   x�# ��Официант
Бармен
\.


�      �   h   x�]�A
�0��_*٤������T��Vld/��]�����hd����<����
��^G5B2�sm9�#��n�(�e�v�jh���d���u��9woG+�      �      x�3�LCKSC3cCKN#�=... Gm�     