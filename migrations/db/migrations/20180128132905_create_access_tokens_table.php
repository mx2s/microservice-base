<?php

use Phinx\Db\Adapter\MysqlAdapter;
use Phinx\Db\Adapter\PostgresAdapter;
use Phinx\Migration\AbstractMigration;

class CreateAccessTokensTable extends AbstractMigration
{
    public function change()
    {
        $table = $this->table('access_tokens');
        $table->addColumn('user_id', 'integer')
            ->addColumn('token', 'string')
            ->addColumn('created_date', 'timestamp', ['default' => 'CURRENT_TIMESTAMP'])
            ->create();
    }
}
